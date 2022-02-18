using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Abstract
{
    public interface IDetectingFailedEmailsJob
    {
        Task DetectFailedEmailsAndSaveAsync();
    }
}
