using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediatorExample {
    public class Worker : BackgroundService {

        // MediatR registers some of its internal services as scoped. Background services get 
        // registered as singletons, so in order to consume scoped services, we have to inject 
        // an IServiceScopeFactory into the constructor, and create our own scope that will 
        // last for the lifetime of the worker task.
        //
        // https://stackoverflow.com/questions/48368634/how-should-i-inject-a-dbcontext-instance-into-an-ihostedservice
        // https://thinkrethink.net/2018/07/12/injecting-a-scoped-service-into-ihostedservice/

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger) {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            using (var scope = _serviceScopeFactory.CreateScope()) {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Publish(new Mediator.Notifications.WorkerStartupNotification(), stoppingToken).ConfigureAwait(false);

                while (!stoppingToken.IsCancellationRequested) {
                    await Task.Delay(5000, stoppingToken).ConfigureAwait(false);
                    var currentTime = await mediator.Send(new Mediator.Requests.GetCurrentTimeRequest(), stoppingToken).ConfigureAwait(false);
                    _logger.LogInformation($"Current time is: {currentTime:dd-MMM-yy HH:mm:ss}");
                }
            }
        }

    }
}
