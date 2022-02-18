using System;
using System.Threading.Tasks;

namespace PCS.BackgroundJobs.Jobs.Abstract
{
    public interface IAccountUnlockJob
    {
        Task UnlockAccountAsync(Guid userId);
    }
}
