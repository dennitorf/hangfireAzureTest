﻿using System;
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
    public class JobController : ControllerBase
    {
        private IJob jobService;

        public JobController(IJob jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            string jobId = BackgroundJob.Enqueue<IJob>(x => x.DoJob("Enqueue One Job"));               
            return Ok(jobId);
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