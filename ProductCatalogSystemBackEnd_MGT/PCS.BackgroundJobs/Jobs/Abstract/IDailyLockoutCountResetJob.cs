using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Abstract
{
    public interface IDailyLockoutCountResetJob
    {
        Task DailyLockoutCountResetAsync();
    }
}
