using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PCS.BackgroundJobs.Jobs.Concrete;

namespace PCS.HangfireApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class RecurringJobController : ControllerBase
    {
        [HttpPost("LockoutCountReset")]
        public IActionResult RegisterDailyLockoutCountResetJob()
        {
            //Daily lockout counts are resetting every midnight by this job
            RecurringJob.AddOrUpdate<DailyLockoutCountResetJob>(nameof(DailyLockoutCountResetJob), 
                x => x.DailyLockoutCountResetAsync(), "0 0 * * *");
            return Ok();
        }

        [HttpPost("DetectingFailedEmails")]
        public IActionResult RegisterDetectingFailedEmailsJob()
        {
            // Detecting failed emails and adding into another table every 5 mins.
            RecurringJob.AddOrUpdate<DetectingFailedEmailsJob>(nameof(DetectingFailedEmailsJob),
                x => x.DetectFailedEmailsAndSaveAsync(), "*/5 * * * *");
            return Ok();
        }

        [HttpDelete("{jobId}")]
        public IActionResult RemoveJobIfExist([FromRoute] string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);
            return Ok();
        }
    }
}
