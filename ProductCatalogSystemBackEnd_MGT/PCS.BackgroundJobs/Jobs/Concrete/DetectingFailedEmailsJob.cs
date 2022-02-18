using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PCS.BackgroundJobs.Jobs.Abstract;
using PCS.Core.Settings;
using PCS.Database.Context.Concrete;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Concrete
{
    public class DetectingFailedEmailsJob : IDetectingFailedEmailsJob
    {
        private readonly IOptions<AppDbSettings> appDbSettingsOptions;

        public DetectingFailedEmailsJob(IOptions<AppDbSettings> appDbSettingsOptions)
        {
            this.appDbSettingsOptions = appDbSettingsOptions;
        }

        public async Task DetectFailedEmailsAndSaveAsync()
        {
            //Creating an UnitOfWork scope to unlock account
            await using var unitOfWork = new UnitOfWork(new PcsDbContext(appDbSettingsOptions));
            //Get failed emails
            var detectedFailedEmails = await unitOfWork.Emails.GetListByExpression(x => x.EmailStatus == EmailStatus.Failed 
            && x.AttemptCount == 5 && !x.IsMoved).ToListAsync();

            if (detectedFailedEmails.Any())
            {
                //If there are failed emails then adding them into 'FailedEmails' table to reporting and re-sending purposes
                var failedEmails = detectedFailedEmails.Select(x => new FailedEmail 
                { EmailId = x.Id, CreatedBy = "Hangfire", CreatedById = $"{nameof(DetectingFailedEmailsJob)}" });
                unitOfWork.FailedEmails.AddList(failedEmails);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
