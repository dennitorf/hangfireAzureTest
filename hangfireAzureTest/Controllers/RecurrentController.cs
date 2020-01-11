using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hangfireAzure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurrentController : ControllerBase
    {
        private IJob jobService;

        public RecurrentController(IJob jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            string jobId = "JOB_SEND_PUSH_SAFER";

            RecurringJob.AddOrUpdate<IJob>(jobId, x => x.DoJob("Recurrrent Job"), Cron.Minutely);

            return Ok();
        }

        [HttpGet("{jobId}")]
        public IActionResult Get(string jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            JobData jobData = connection.GetJobData(jobId);

            var obj = new
            {
                State = jobData.State,
                CreatedAt = jobData.CreatedAt
            };

            return Ok(obj);
        }
    }
}