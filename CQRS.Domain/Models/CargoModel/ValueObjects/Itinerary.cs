using EventFlow.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.ValueObjects
{
    public class Itinerary : ValueObject
    {
        public Itinerary(
            IEnumerable<TransportLeg> transportLegs)
        {
            var legsList = (transportLegs ?? Enumerable.Empty<TransportLeg>()).ToList();

            if (!legsList.Any()) throw new ArgumentException(nameof(transportLegs));
            (new TransportLegsAreConnectedSpecification()).ThrowDomainErrorIfNotStatisfied(legsList);

            TransportLegs = legsList;
        }

        public IReadOnlyList<TransportLeg> TransportLegs { get; }

        public LocationId DepartureLocation()
        {
            return TransportLegs.First().LoadLocation;
        }

        public DateTimeOffset DepartureTime()
        {
            return TransportLegs.First().UnloadTime;
        }

        public DateTimeOffset ArrivalTime()
        {
            return TransportLegs.Last().UnloadTime;
        }

        public LocationId ArrivalLocation()
        {
            return TransportLegs.Last().UnloadLocation;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return TransportLegs;
        }
    }
}