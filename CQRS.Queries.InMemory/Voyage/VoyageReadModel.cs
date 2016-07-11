using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Events;
using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Voyage
{
    public class VoyageReadModel : IReadModel,
        IAmReadModelFor<VoyageAggregate, VoyageId, VoyageCreatedEvent>,
        IAmReadModelFor<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent>
    {
        public VoyageId Id { get; private set; }
        public Schedule Schedule { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageCreatedEvent> e)
        {
            Id = e.AggregateIdentity;
            Schedule = e.AggregateEvent.Schedule;
        }

        public void Apply(IReadModelContext context, IDomainEvent<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent> domainEvent)
        {
            Schedule = domainEvent.AggregateEvent.Schedule;
        }

        public Domain.Models.VoyageModel.Voyage ToVoyage()
        {
            return new Domain.Models.VoyageModel.Voyage(
                Id,
                Schedule);
        }
    }
}