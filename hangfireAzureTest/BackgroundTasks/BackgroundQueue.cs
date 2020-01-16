using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hangfireAzure.Api.BackgroundTasks
{
    public interface IBackgroundQueue
    {
        void Push(Func<CancellationToken, Task> task);
        Task<Func<CancellationToken, Task>> Pop(CancellationToken cancellationToken);
    }
    public class BackgroundQueue : IBackgroundQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> tasksQueue;
        private SemaphoreSlim signal;

        public BackgroundQueue()
        {
            tasksQueue = new ConcurrentQueue<Func<CancellationToken, Task>>();
            signal = new SemaphoreSlim(0);
        }

        public async Task<Func<CancellationToken, Task>> Pop(CancellationToken cancellationToken)
        {
            await signal.WaitAsync(cancellationToken);
            tasksQueue.TryDequeue(out var task);
            return task;
        }

        public void Push(Func<CancellationToken, Task> task)
        {
            tasksQueue.Enqueue(task);
            signal.Release();
        }
    }
}
