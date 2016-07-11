using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.LocationModel.Queries
{
    public class GetLocationsQuery : IQuery<IReadOnlyCollection<Location>>
    {
        public IReadOnlyCollection<LocationId> LocationIds { get; }

        public GetLocationsQuery(params LocationId[] locationIds)
            : this((IEnumerable<LocationId>)locationIds)
        {
        }

        public GetLocationsQuery(IEnumerable<LocationId> locationIds)
        {
            LocationIds = locationIds.ToList();
        }
    }
}