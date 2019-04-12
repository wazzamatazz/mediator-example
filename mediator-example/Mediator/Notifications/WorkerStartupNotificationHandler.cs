using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatorExample.Mediator.Notifications {
    public class WorkerStartupNotificationHandler : INotificationHandler<WorkerStartupNotification> {

        private readonly ILogger _logger;


        public WorkerStartupNotificationHandler(ILogger<WorkerStartupNotificationHandler> logger) {
            _logger = logger;
        }


        public Task Handle(WorkerStartupNotification notification, CancellationToken cancellationToken) {
            _logger.LogWarning($"Background worker started up at {notification.StartupTime:HH:mm:ss}.");
            return Task.CompletedTask;
        }
    }
}
