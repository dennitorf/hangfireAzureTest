using hangfireAzure.EndpointOrchestator;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace hangfireAzure
{
    public class Job : IJob
    {
        private ISignalOrchestatorService signalOrchestator;

        public Job(ISignalOrchestatorService signalOrchestator)
        {
            this.signalOrchestator = signalOrchestator;
        }

        public async void DoJob(string executedFrom)
        {
            

            await Task.Run(() => {
                Random r = new Random();

                Console.WriteLine("Enviar mensaje de Pushsafer");

                var options = new NameValueCollection {
                    { "t", "PushSafer Testing with Hangfire + Azure" },
                    { "m", "Testing Message "  + DateTime.Now.ToString() + " executed from " + executedFrom },
                    { "s", "" },
                    { "v", "" },
                    { "i", "" },
                    { "c", String.Format("{0}", ((r.Next() % 177) + 1))},
                    { "d", "a" },
                    { "u", "" },
                    { "ut", "" },
                    { "p", "" },
                    { "k", "CmHhxcbjTINMpyicdlbg" }
            };
                using (var client = new WebClient())
                {
                    client.UploadValues("https://www.pushsafer.com/api", options);                    
                }

                signalOrchestator.Success();
            });
            
        }
        
    }

    public interface IJob 
    {
        void DoJob(string executedFrom);
    }
}
