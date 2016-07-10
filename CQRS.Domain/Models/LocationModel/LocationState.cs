using CQRS.Domain.Models.LocationModel.Events;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.LocationModel
{
    public class LocationState :
        AggregateState<LocationAggregate, LocationId, LocationState>,
        IApply<LocationCreatedEvent>
    {
        public string Name { get; private set; }

        public void Apply(LocationCreatedEvent aggregateEvent)
        {
            Name = aggregateEvent.Name;
        }
    }
}