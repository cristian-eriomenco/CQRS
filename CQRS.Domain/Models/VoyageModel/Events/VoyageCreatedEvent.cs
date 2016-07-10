using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.Events
{
    [EventVersion("VoyageCreated", 1)]
    public class VoyageCreatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public VoyageCreatedEvent(
            Schedule schedule)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }
}