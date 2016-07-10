using CQRS.Domain.Models.LocationModel.Events;
using EventFlow.Aggregates;
using EventFlow.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.LocationModel
{
    public class LocationAggregate : AggregateRoot<LocationAggregate, LocationId>
    {
        private readonly LocationState _state = new LocationState();

        public LocationAggregate(LocationId id) : base(id)
        {
            Register(_state);
        }

        public void Create(string name)
        {
            if (!IsNew) throw DomainError.With("Location is already created");
            Emit(new LocationCreatedEvent(name));
        }
    }
}