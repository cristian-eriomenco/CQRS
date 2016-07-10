using CQRS.Domain.Models.VoyageModel.Events;
using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel
{
    public class VoyageState :
        AggregateState<VoyageAggregate, VoyageId, VoyageState>,
        IApply<VoyageCreatedEvent>,
        IApply<VoyageScheduleUpdatedEvent>
    {
        public Schedule Schedule { get; private set; }

        public void Apply(VoyageCreatedEvent aggregateEvent)
        {
            Schedule = aggregateEvent.Schedule;
        }

        public void Apply(VoyageScheduleUpdatedEvent aggregateEvent)
        {
            Schedule = aggregateEvent.Schedule;
        }
    }
}