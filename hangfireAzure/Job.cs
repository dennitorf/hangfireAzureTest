using System;
using System.Collections.Specialized;
using System.Net;

namespace hangfireAzure
{
    public class Job : IJob
    {
        public void DoJob(string executedFrom)
        {

            Random r = new Random();

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
                    { "k", "k4FvK1KRmQm161Cv9bKb" }
            };
            using (var client = new WebClient())
            {
                client.UploadValues("https://www.pushsafer.com/api", options);
            }
        }
    }

    public interface IJob
    {
        void DoJob(string executedFrom);
    }
}
