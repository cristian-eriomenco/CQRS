using CQRS.Domain.Models.CargoModel.Events;
using CQRS.Domain.Models.CargoModel.ValueObjects;
using EventFlow.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel
{
    public class CargoState :
        AggregateState<CargoAggregate, CargoId, CargoState>,
        IApply<CargoBookedEvent>,
        IApply<CargoItinerarySetEvent>
    {
        public Route Route { get; private set; }
        public Itinerary Itinerary { get; private set; }

        public void Apply(CargoBookedEvent aggregateEvent)
        {
            Route = aggregateEvent.Route;
        }

        public void Apply(CargoItinerarySetEvent aggregateEvent)
        {
            Itinerary = aggregateEvent.Itinerary;
        }
    }
}