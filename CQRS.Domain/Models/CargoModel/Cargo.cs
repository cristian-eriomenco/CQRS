using CQRS.Domain.Models.CargoModel.ValueObjects;
using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel
{
    public class Cargo : Entity<CargoId>
    {
        public Cargo(
            CargoId id,
            Route route,
            Itinerary itinerary)
            : base(id)
        {
            Route = route;
            Itinerary = itinerary;
        }

        public Route Route { get; }
        public Itinerary Itinerary { get; }
    }
}