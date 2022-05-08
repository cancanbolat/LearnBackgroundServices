using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.BackgroundServices
{
    public class DateTimeLogWriter : IHostedService, IDisposable
    {
        private Timer timer;

        // uygulama çalıştığında burası da çalışır. uygulama durana kadar.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(DateTimeLogWriter)} service STARTED");

            timer = new Timer(WriteDateTimeOnLog, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void WriteDateTimeOnLog(object state)
        {
            Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
        }

        // uygulama durduğunda burası çalışır.
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0); //app durunca timer'i de durdur.

            Console.WriteLine($"{nameof(DateTimeLogWriter)} service STOPPED");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer = null;
        }
    }

    public class DateTimeLogWriter2 : BackgroundService
    {

        // start ve stop'u override edebilirsin.
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
