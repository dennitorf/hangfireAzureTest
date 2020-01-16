using System;
using System.Collections.Generic;
using System.Text;

namespace hangfireAzure.EndpointOrchestator
{
    public interface ISignalOrchestatorService
    {
        bool Success();
    }

    public class SignalOrchestatorService : ISignalOrchestatorService
    {
        private string orchestatorUri { set; get; }

        public SignalOrchestatorService(string orchestatorUri)
        {
            this.orchestatorUri = orchestatorUri;
        }

        public bool Success()
        {
            Console.WriteLine("Post into " + this.orchestatorUri + " done!!!");

            return true;
        }
    }
}
