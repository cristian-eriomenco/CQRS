using CQRS.Domain.Models.VoyageModel.Entities;
using EventFlow.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.ValueObjects
{
    public class Schedule : ValueObject
    {
        public Schedule(
            IEnumerable<CarrierMovement> carrierMovements)
        {
            var carrierMovementList = (carrierMovements ?? Enumerable.Empty<CarrierMovement>()).ToList();

            if (!carrierMovementList.Any()) throw new ArgumentException(nameof(carrierMovements));

            CarrierMovements = carrierMovementList;
        }

        public IReadOnlyList<CarrierMovement> CarrierMovements { get; }

        public Schedule Delay(TimeSpan delay)
        {
            var carrierMovements = CarrierMovements
                .Select(m => new CarrierMovement(
                    m.Id,
                    m.DepartureLocationId,
                    m.ArrivalLocationId,
                    m.DepartureTime + delay,
                    m.ArrivalTime + delay));
            return new Schedule(carrierMovements);
        }
    }
}