using CQRS.Domain.Models.CargoModel.ValueObjects;
using EventFlow.Aggregates;
using EventFlow.EventStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Events
{
    [EventVersion("CargoBooked", 1)]
    public class CargoBookedEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public CargoBookedEvent(
            Route route)
        {
            Route = route;
        }

        public Route Route { get; }
    }
}