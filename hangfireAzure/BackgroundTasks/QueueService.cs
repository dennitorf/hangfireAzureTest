using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hangfireAzure.BackgroundTasks
{
    public class QueueService : BackgroundService
    {
        IBackgroundQueue queue;

        public QueueService(IBackgroundQueue queue)
        {
            this.queue = queue;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                var task = await queue.Pop(stoppingToken);
                await task(stoppingToken);
            }
        }
    }
}
