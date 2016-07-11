using CQRS.Domain.Models.CargoModel;
using CQRS.Domain.Models.CargoModel.Queries;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Queries.InMemory.Cargos.Queries
{
    public class GetCargosDependentOnVoyageQueryHandler : IQueryHandler<GetCargosDependentOnVoyageQuery, IReadOnlyCollection<Cargo>>
    {
        private readonly IInMemoryReadStore<CargoReadModel> _readStore;

        public GetCargosDependentOnVoyageQueryHandler(
            IInMemoryReadStore<CargoReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosDependentOnVoyageQuery query, CancellationToken cancellationToken)
        {
            var cargoReadModels = await _readStore.FindAsync(
                rm => rm.DependentVoyageIds.Contains(query.VoyageId),
                cancellationToken)
                .ConfigureAwait(false);
            return cargoReadModels.Select(rm => rm.ToCargo()).ToList();
        }
    }
}