using hangfireAzure.EndpointOrchestator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureOrchestator
    {
        public static IServiceCollection AddSignalOrchestator(this IServiceCollection services, string uri)
        {            
            services.AddSingleton<ISignalOrchestatorService>(x => new SignalOrchestatorService(uri));

            return services;
        }
    }
}
