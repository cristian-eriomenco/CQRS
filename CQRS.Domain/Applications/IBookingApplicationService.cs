using CQRS.Domain.Models.CargoModel;
using CQRS.Domain.Models.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Applications
{
    public interface IBookingApplicationService
    {
        Task<CargoId> BookCargoAsync(Route route, CancellationToken cancellationToken);
    }
}