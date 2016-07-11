using CQRS.Domain.Models.LocationModel;
using CQRS.Domain.Models.LocationModel.Events;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Locations
{
    public class LocationReadModel : IReadModel,
        IAmReadModelFor<LocationAggregate, LocationId, LocationCreatedEvent>
    {
        public LocationId Id { get; set; }
        public string Name { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<LocationAggregate, LocationId, LocationCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity;
            Name = domainEvent.AggregateEvent.Name;
        }

        public Location ToLocation()
        {
            return new Location(Id, Name);
        }
    }
}