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
    [EventVersion("CargoItinerarySet", 1)]
    public class CargoItinerarySetEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public CargoItinerarySetEvent(
            Itinerary itinerary)
        {
            Itinerary = itinerary;
        }

        public Itinerary Itinerary { get; }
    }
}