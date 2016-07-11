using CQRS.Domain.Models.LocationModel;
using CQRS.Domain.Models.LocationModel.Queries;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Locations.Queries
{
    public class GetLocationsQueryHandler : IQueryHandler<GetLocationsQuery, IReadOnlyCollection<Location>>
    {
        private readonly IInMemoryReadStore<LocationReadModel> _readStore;

        public GetLocationsQueryHandler(
            IInMemoryReadStore<LocationReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Location>> ExecuteQueryAsync(GetLocationsQuery query, CancellationToken cancellationToken)
        {
            var locationIds = new HashSet<LocationId>(query.LocationIds);

            var locationReadModels = await _readStore.FindAsync(
                rm => !locationIds.Any() || locationIds.Contains(rm.Id),
                cancellationToken)
                .ConfigureAwait(false);

            return locationReadModels.Select(rm => rm.ToLocation()).ToList();
        }
    }
}