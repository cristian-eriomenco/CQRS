using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Commands;
using EventFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Applications
{
    public class ScheduleApplicationService : IScheduleApplicationService
    {
        private readonly ICommandBus _commandBus;

        public ScheduleApplicationService(
            ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Task DelayScheduleAsync(VoyageId voyageId, TimeSpan delay, CancellationToken cancellationToken)
        {
            return _commandBus.PublishAsync(new VoyageDelayCommand(voyageId, delay), cancellationToken);
        }
    }
}