using CQRS.Domain.Models.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Services
{
    public interface IRoutingService
    {
        Task<IReadOnlyCollection<Itinerary>> CalculateItinerariesAsync(Route route, CancellationToken cancellationToken);
    }
}