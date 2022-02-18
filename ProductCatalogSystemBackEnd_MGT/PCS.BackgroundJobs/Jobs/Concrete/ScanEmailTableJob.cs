using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using PCS.BackgroundJobs.Jobs.Abstract;
using PCS.Core.Settings;
using PCS.Database.Context.Concrete;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Concrete
{
    public class ScanEmailTableJob : IScanEmailTableJob
    {
        private readonly SmtpSettings smtpSettings;
        private readonly IOptions<AppDbSettings> dbSettingsOptions;

        public ScanEmailTableJob(IOptions<SmtpSettings> smtpSettingsOptions, IOptions<AppDbSettings> dbSettingsOptions)
        {
            smtpSettings = smtpSettingsOptions.Value;
            this.dbSettingsOptions = dbSettingsOptions;
        }

        public async Task ScanEmailsAndSendAsync()
        {
            //Creating an UnitOfWork scope to unlock account
            await using var unitOfWork = new UnitOfWork(new PcsDbContext(dbSettingsOptions));
            //Get all emails that are waiting to send
            var emails = await unitOfWork.Emails.GetListByExpression(x => x.EmailStatus == EmailStatus.InProcess
            && x.AttemptCount < 5).Include(x => x.Receiver).ToListAsync();
            if (emails.Any())
            {
                await SendEmailsAsync(emails, unitOfWork);
            }        
        }

        private async Task SendEmailsAsync(List<Email> emails, UnitOfWork unitOfWork)
        {
            //Creating smtp client and connecting to smtp server with that
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(smtpSettings.Server, smtpSettings.Port, smtpSettings.EnableSsl);
            await smtpClient.AuthenticateAsync(smtpSettings.EmailAddress, smtpSettings.Password);

            foreach (var email in emails)
            {
                //For each email, creating a mail message and sending it. Then updating the situation of mails.
                email.AttemptCount++;
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(smtpSettings.EmailAddress));

                if (email.EmailType == EmailType.Welcome)
                {
                    PrepareWelcomeMessage(email.Receiver, message);
                }

                if (email.EmailType == EmailType.LockoutNotification)
                {
                    PrepareLockoutMessage(email.Receiver, message);
                }

                if (email.EmailType == EmailType.PermaBlockNotification)
                {
                    PreparePermaBlockMessage(email.Receiver, message);
                }

                var response = await smtpClient.SendAsync(message);

                if (response.Contains("OK"))
                {
                    email.EmailStatus = EmailStatus.Sent;
                }
                else
                {
                    if (email.AttemptCount == 5)
                    {
                        email.EmailStatus = EmailStatus.Failed;
                    }
                }

                email.MailResponse = response;
                email.MailResponseDate = DateTime.UtcNow;
                email.UpdatedBy = "Email Worker Service";
                email.UpdatedById = $"{nameof(ScanEmailTableJob)}";
                unitOfWork.Emails.Update(email);
                message.Dispose();
            }
            await smtpClient.DisconnectAsync(true);
            await unitOfWork.SaveAsync();
        }

        private static void PrepareWelcomeMessage(User user, MimeMessage message)
        {
            message.To.Add(MailboxAddress.Parse(user.EmailAddress));
            message.Subject = "Welcome to Pcs App";
            message.Body = new TextPart("plain")
            {
                Text = $"{user.Firstname + " " + user.Lastname},\n" +
                $"We are happy to see you among us. Enjoy your shopping."
            };
        }

        private static void PrepareLockoutMessage(User user, MimeMessage message)
        {
            message.To.Add(MailboxAddress.Parse(user.EmailAddress));
            message.Subject = "Lockout Notification - Pcs App";
            message.Body = new TextPart("plain")
            {
                Text = $"{user.Firstname + " " + user.Lastname},\n" +
                $"We detected unusual login attempts. Your account has been locked out for 10 mins.\n"
            };
        }
        private static void PreparePermaBlockMessage(User user, MimeMessage message)
        {
            message.To.Add(MailboxAddress.Parse(user.EmailAddress));
            message.Subject = "Perma Block Notification - Pcs App";
            message.Body = new TextPart("plain")
            {
                Text = $"{user.Firstname + " " + user.Lastname},\n" +
                $"We detected unusual login attempts. Your account has been permanently blocked.\n" +
                $"Please contact us."
            };
        }
    }
}

