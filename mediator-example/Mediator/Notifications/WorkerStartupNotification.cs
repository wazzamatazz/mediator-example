using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MediatorExample.Mediator.Notifications {
    public class WorkerStartupNotification : INotification {

        public DateTime StartupTime { get; set; } = DateTime.Now;

    }

}
