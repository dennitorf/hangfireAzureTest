using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using hangfireAzure.Api.BackgroundTasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace hangfireAzure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IJob jobService;
        private IBackgroundQueue backgroundQueue;

        public JobController(IJob jobService, IBackgroundQueue backgroundQueue)
        {
            this.jobService = jobService;
            this.backgroundQueue = backgroundQueue;
        }

        [HttpPost]
        public IActionResult Post()
        {
            backgroundQueue.Push(async token => {
                await Task.Run(() => jobService.DoJob("From Queue"));
            });
            return StatusCode(202);
        }

        [HttpGet("{jobId}")]
        public IActionResult Get(string jobId)
        {
            //IStorageConnection connection = JobStorage.Current.GetConnection();
            //JobData jobData = connection.GetJobData(jobId);

            //var obj = new
            //{
            //    State = jobData.State,
            //    CreatedAt = jobData.CreatedAt                
            //};

            return Ok(1);
        }
    }
}