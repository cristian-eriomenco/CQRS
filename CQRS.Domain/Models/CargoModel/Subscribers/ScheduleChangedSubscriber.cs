using CQRS.Domain.Models.CargoModel.Jobs;
using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Events;
using EventFlow.Aggregates;
using EventFlow.Jobs;
using EventFlow.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Subscribers
{
    public class ScheduleChangedSubscriber :
        ISubscribeSynchronousTo<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent>
    {
        private readonly IJobScheduler _jobScheduler;

        public ScheduleChangedSubscriber(
            IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        public Task HandleAsync(IDomainEvent<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent> domainEvent, CancellationToken cancellationToken)
        {
            var job = new VerifyCargosForVoyageJob(
                domainEvent.AggregateIdentity);
            return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
        }
    }
}