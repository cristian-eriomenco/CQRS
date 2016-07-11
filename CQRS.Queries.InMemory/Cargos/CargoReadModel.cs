using CQRS.Domain.Models.CargoModel;
using CQRS.Domain.Models.CargoModel.Events;
using CQRS.Domain.Models.CargoModel.ValueObjects;
using CQRS.Domain.Models.VoyageModel;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Cargos
{
    public class CargoReadModel : IReadModel,
        IAmReadModelFor<CargoAggregate, CargoId, CargoItinerarySetEvent>,
        IAmReadModelFor<CargoAggregate, CargoId, CargoBookedEvent>
    {
        public CargoId Id { get; private set; }
        public HashSet<VoyageId> DependentVoyageIds { get; } = new HashSet<VoyageId>();
        public Itinerary Itinerary { get; private set; }
        public Route Route { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, CargoBookedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity;
            Route = domainEvent.AggregateEvent.Route;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CargoAggregate, CargoId, CargoItinerarySetEvent> domainEvent)
        {
            Itinerary = domainEvent.AggregateEvent.Itinerary;
            foreach (var transportLeg in domainEvent.AggregateEvent.Itinerary.TransportLegs)
            {
                DependentVoyageIds.Add(transportLeg.VoyageId);
            }
        }

        public Cargo ToCargo()
        {
            return new Cargo(
                Id,
                Route,
                Itinerary);
        }
    }
}