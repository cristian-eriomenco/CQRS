using CQRS.Domain.Models.VoyageModel;
using CQRS.Domain.Models.VoyageModel.Queries;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Voyage.Queries
{
    public class GetVoyagesQueryHandler : IQueryHandler<GetVoyagesQuery, IReadOnlyCollection<Domain.Models.VoyageModel.Voyage>>
    {
        private readonly IInMemoryReadStore<VoyageReadModel> _readStore;

        public GetVoyagesQueryHandler(
            IInMemoryReadStore<VoyageReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Domain.Models.VoyageModel.Voyage>> ExecuteQueryAsync(GetVoyagesQuery query, CancellationToken cancellationToken)
        {
            var voyageIds = new HashSet<VoyageId>(query.VoyageIds);
            var voyageReadModels = await _readStore.FindAsync(rm => voyageIds.Contains(rm.Id), cancellationToken).ConfigureAwait(false);
            return voyageReadModels.Select(rm => rm.ToVoyage()).ToList();
        }
    }
}