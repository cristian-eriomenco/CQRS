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
    public class GetAllVoyagesQueryHandler : IQueryHandler<GetAllVoyagesQuery, IReadOnlyCollection<Domain.Models.VoyageModel.Voyage>>
    {
        private readonly IInMemoryReadStore<VoyageReadModel> _readStore;

        public GetAllVoyagesQueryHandler(
            IInMemoryReadStore<VoyageReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Domain.Models.VoyageModel.Voyage>> ExecuteQueryAsync(GetAllVoyagesQuery query, CancellationToken cancellationToken)
        {
            var voyageReadModels = await _readStore.FindAsync(rm => true, cancellationToken).ConfigureAwait(false);
            return voyageReadModels.Select(rm => rm.ToVoyage()).ToList();
        }
    }
}