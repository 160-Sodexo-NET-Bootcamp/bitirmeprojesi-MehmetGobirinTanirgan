using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PCS.BackgroundJobs.Jobs.Abstract;
using PCS.Core.Settings;
using PCS.Database.Context.Concrete;
using PCS.Repository.UnitOfWork.Concrete;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Concrete
{
    public class DailyLockoutCountResetJob : IDailyLockoutCountResetJob
    {
        private readonly IOptions<AppDbSettings> appDbSettingsOptions;

        public DailyLockoutCountResetJob(IOptions<AppDbSettings> appDbSettingsOptions)
        {
            this.appDbSettingsOptions = appDbSettingsOptions;
        }

        public async Task DailyLockoutCountResetAsync()
        { 
            //Creating an UnitOfWork scope to unlock account
            await using var unitOfWork = new UnitOfWork(new PcsDbContext(appDbSettingsOptions));
            //Get all users where the user has not been locked out more than 2 times for the current day
            var users = await unitOfWork.Users.GetListByExpression(x => !x.PermaBlockEnabled && x.DailyLockoutCount > 0).ToListAsync();
            if (users.Any())
            {
                // Resetting daily lockout count and saving it
                users.ForEach(x =>
                {
                    x.DailyLockoutCount = 0;
                    x.UpdatedBy = "Hangfire";
                    x.UpdatedById = $"{nameof(DailyLockoutCountResetJob)}";
                });
                unitOfWork.Users.UpdateList(users);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
