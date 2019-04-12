using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MediatorExample.Mediator.Requests {

    public class GetCurrentTimeRequest : IRequest<DateTime> { }


    public class GetCurrentTimeHandler : IRequestHandler<GetCurrentTimeRequest, DateTime> {

        public Task<DateTime> Handle(GetCurrentTimeRequest request, CancellationToken cancellationToken) {
            return Task.FromResult(DateTime.Now);
        }

    }

}
