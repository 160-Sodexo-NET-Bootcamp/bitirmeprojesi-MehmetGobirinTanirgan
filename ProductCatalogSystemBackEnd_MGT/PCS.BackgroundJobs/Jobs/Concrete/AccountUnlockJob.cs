using Microsoft.Extensions.Options;
using PCS.BackgroundJobs.Jobs.Abstract;
using PCS.Core.Settings;
using PCS.Database.Context.Concrete;
using PCS.Repository.UnitOfWork.Concrete;
using System;
using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Concrete
{
    public class AccountUnlockJob : IAccountUnlockJob
    {
        private readonly IOptions<AppDbSettings> appDbSettingsOptions;

        public AccountUnlockJob(IOptions<AppDbSettings> appDbSettingsOptions)
        {
            this.appDbSettingsOptions = appDbSettingsOptions;
        }

        public async Task UnlockAccountAsync(Guid userId)
        {
            //Creating an UnitOfWork scope to unlock account
            await using var unitOfWork = new UnitOfWork(new PcsDbContext(appDbSettingsOptions));
            var existingUser = await unitOfWork.Users.GetByIdAsync(userId);//Get user
            if (!existingUser.PermaBlockEnabled)//If it's not perma blocked
            {
                //Then unlock account and save it
                existingUser.LockoutEnabled = false;
                existingUser.UpdatedBy = "Account Unlock Worker Service";
                existingUser.UpdatedById = $"{nameof(AccountUnlockJob)}";
                unitOfWork.Users.Update(existingUser);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
