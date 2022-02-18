using AutoMapper;
using Microsoft.Extensions.Options;
using PCS.Core.CustomExceptions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Settings;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.UserDtos.Request;
using PCS.Entity.Dtos.UserDtos.Response;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.RabbitMq.Publishers;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly JwtSettings jwtSettings;
        private readonly RabbitMqSettings rabbitMqSettings;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSettings> jwtSettingsOptions, IOptions<RabbitMqSettings> rabbitMqOptions)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            jwtSettings = jwtSettingsOptions.Value;
            rabbitMqSettings = rabbitMqOptions.Value;
        }

        public async Task<IResult> RegisterAsync(UserCreateDto userCreateDto)
        {
            // Checking if there is an account registered with same mail or phone number.
            var doesUserExist = await unitOfWork.Users.AnyAsync(x => x.EmailAddress == userCreateDto.EmailAddress || x.PhoneNumber == userCreateDto.PhoneNumber);

            //If exist
            if (doesUserExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E1);
            }

            var newUser = mapper.Map<User>(userCreateDto);// Create user model
            newUser.PasswordSalt = PasswordHashingHelper.GenerateSalt(); //Generate a password salt
            newUser.HashedPassword = PasswordHashingHelper.HashPassword(userCreateDto.Password, newUser.PasswordSalt);// Hash the pasword with salt
            newUser.Emails = new List<Email> { new Email { EmailStatus = EmailStatus.InProcess, EmailType = EmailType.Welcome } };// Add a new welcome mail
            unitOfWork.Users.Add(newUser);//Add user
            await unitOfWork.SaveAsync();
            return ResultGenerator.Created(SuccessMessages.S1);
        }

        public async Task<IResult> LoginAsync(UserLoginDto userLoginDto)
        {
            //Get user with email address
            var existingUser = await unitOfWork.Users.GetByExpressionAsync(x => x.EmailAddress == userLoginDto.EmailAddress);

            if (existingUser is null)//If not exist then return error
            {
                return ResultGenerator.NotFound(ErrorMessages.E2);
            }

            if (existingUser.PermaBlockEnabled)//If account is blocked permanently then return error
            {
                return ResultGenerator.BadRequest(ErrorMessages.E3);
            }

            if (existingUser.LockoutEnabled)// If account is locked out temporarly then return error
            {
                return ResultGenerator.BadRequest(ErrorMessages.E5);
            }

            //if (!existingUser.EmailConfirmed) //If email is not confirmed, then return error
            //{
            //    return ResultGenerator.BadRequest(ErrorMessageList.E6);
            //}

            //Checking password accuracy
            var isPasswordCorrect = PasswordHashingHelper.VerifyPassword(userLoginDto.Password, existingUser.HashedPassword,
                existingUser.PasswordSalt);

            if (!isPasswordCorrect)//If password is not correct
            {
                existingUser.FailedLoginAttemptCount++; //login attempt counter
                existingUser.UpdatedBy = "Login failure";
                existingUser.UpdatedById = "Login failure";
                if (existingUser.FailedLoginAttemptCount % 3 == 0)//Lockout the account every third failed login attempt
                {
                    existingUser.DailyLockoutCount++;// daily lockout counter
                    //If daily lockout count reaches to 3, it means totally 9
                    //failed login attempt for today, then we have to block the account permanently for safety purposes
                    if (existingUser.DailyLockoutCount == 3)
                    {
                        existingUser.PermaBlockEnabled = true; //Enabling perma block
                        unitOfWork.Users.Update(existingUser);
                        //Adding an email for sending perma block notification to account owner
                        unitOfWork.Emails.Add(new Email
                        {
                            ReceiverId = existingUser.Id,
                            EmailStatus = EmailStatus.InProcess,
                            EmailType = EmailType.PermaBlockNotification
                        });
                        await unitOfWork.SaveAsync();
                        return ResultGenerator.BadRequest(ErrorMessages.E3);
                    }

                    //If daily lockout counter has not reach the 3, then we have to lock out the account for 10 mins.
                    existingUser.LockoutEnabled = true;
                    unitOfWork.Users.Update(existingUser);

                    var lockoutRecord = new LockoutRecord
                    {
                        LockoutUntil = DateTime.UtcNow.AddMinutes(10),// 10 mins until unlock
                        LockoutBegin = DateTime.UtcNow,
                        UserId = existingUser.Id
                    };

                    //Lockout notification email
                    unitOfWork.Emails.Add(new Email
                    {
                        ReceiverId = existingUser.Id,
                        EmailStatus = EmailStatus.InProcess,
                        EmailType = EmailType.LockoutNotification
                    });

                    //Publishing lockout record data by rabbitmq
                    AccountUnlockJobPublisher.Publish(lockoutRecord, rabbitMqSettings);
                    unitOfWork.LockoutRecords.Add(lockoutRecord);
                    await unitOfWork.SaveAsync();
                    return ResultGenerator.BadRequest(ErrorMessages.E4);
                }
                unitOfWork.Users.Update(existingUser);
                await unitOfWork.SaveAsync();
                return ResultGenerator.NotFound(ErrorMessages.E2);
            }                
            return await ReturnSuccessLoginResultAsync(existingUser);
        }

        public async Task<IResult> RefreshLoginAsync(UserRefreshLoginDto userRefreshLoginDto)
        {
            var existingUser = await unitOfWork.Users.GetByExpressionAsync(x => x.EmailAddress == userRefreshLoginDto.EmailAddress);

            if (existingUser is null)// Checking account
            {
                throw new ConflictException(ExceptionMessages.A1(userRefreshLoginDto.EmailAddress));
            }

            var isRefreshTokenValid = existingUser.RefreshToken == userRefreshLoginDto.RefreshToken
                && existingUser.RefreshTokenExpireDate > DateTime.UtcNow;

            if (!isRefreshTokenValid)//Checking refresh token's validation
            {
                return ResultGenerator.NotFound(ErrorMessages.E16);
            }

            return await ReturnSuccessLoginResultAsync(existingUser);
        }

        public async Task<IResult> ResetPasswordAsync(UserResetPasswordDto userResetPasswordDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingUser = await unitOfWork.Users.GetByExpressionAsync(x => x.EmailAddress == userResetPasswordDto.EmailAddress);

            if (existingUser is null)//Checking account
            {
                return ResultGenerator.NotFound();
            }

            if (existingUser.Id != checkableEntityHelper.MainUserId)//Checking request owner and account owner same or not
            {
                throw new ConflictException(ExceptionMessages.A3(checkableEntityHelper.MainUserId,existingUser.Id));
            }

            if (!PasswordHashingHelper.VerifyPassword(userResetPasswordDto.CurrentPassword,
                existingUser.HashedPassword, existingUser.PasswordSalt))// Checking current password's accuracy
            {
                throw new ConflictException(ExceptionMessages.A4(checkableEntityHelper.MainUserId));
            }

            var newSalt = PasswordHashingHelper.GenerateSalt();
            existingUser.HashedPassword = PasswordHashingHelper.HashPassword(userResetPasswordDto.NewPassword, newSalt);
            existingUser.PasswordSalt = newSalt;
            checkableEntityHelper.AddMainUserAsUpdater(existingUser);
            unitOfWork.Users.Update(existingUser);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok(SuccessMessages.S2);
        }

        private async Task<IResult> ReturnSuccessLoginResultAsync(User existingUser)
        {
            //Generating jwt token and returning success result
            var claimsIdentity = new ClaimsIdentity
            (new[]
                {
                    new Claim(ClaimTypes.Role,existingUser.Role),
                    new Claim("userId",existingUser.Id.ToString()),
                    new Claim("fullname",existingUser.Firstname + " " + existingUser.Lastname),
                });

            var userLoginResponseDto = mapper.Map<UserLoginResponseDto>(existingUser);
            var expireDateOfJwt = DateTime.UtcNow.AddHours(1);
            var authToken = TokenGenerator.GenerateJwtToken(jwtSettings, claimsIdentity, expireDateOfJwt);
            userLoginResponseDto.AuthenticationToken = authToken;
            userLoginResponseDto.RefreshToken = TokenGenerator.GenerateRefreshToken();
            userLoginResponseDto.AuthenticationTokenExpireDate = expireDateOfJwt;
            existingUser.RefreshToken = userLoginResponseDto.RefreshToken;
            existingUser.RefreshTokenExpireDate = expireDateOfJwt.AddMinutes(5);
            unitOfWork.Users.Update(existingUser);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok(userLoginResponseDto);
        }
    }
}
