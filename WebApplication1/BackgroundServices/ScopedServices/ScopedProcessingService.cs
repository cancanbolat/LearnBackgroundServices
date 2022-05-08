using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.BackgroundServices.ScopedServices
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    public class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<ScopedProcessingService> logger;
        private int executionCount = 0;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            this.logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                logger.LogInformation($"Scoped Processing service is working. Count: {executionCount}");

                await Task.Delay(10000, stoppingToken);
            }
        }
    }


}
