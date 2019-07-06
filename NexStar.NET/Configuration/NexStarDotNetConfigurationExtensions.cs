using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NexStar.Net.Configuration
{
    public static class NexStarDotNetConfigurationExtensions
    {
        public static void AddNexStarDotNet(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INexStarConnectionManager, NexStarConnectionManager>();
        }
    }
}
