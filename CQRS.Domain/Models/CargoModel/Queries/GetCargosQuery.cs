using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Queries
{
    public class GetCargosQuery : IQuery<IReadOnlyCollection<Cargo>>
    {
        public GetCargosQuery(
            params CargoId[] cargoIds)
            : this((IEnumerable<CargoId>)cargoIds)
        {
        }

        public GetCargosQuery(IEnumerable<CargoId> cargoIds)
        {
            CargoIds = cargoIds.ToList();
        }

        public IReadOnlyCollection<CargoId> CargoIds { get; }
    }
}