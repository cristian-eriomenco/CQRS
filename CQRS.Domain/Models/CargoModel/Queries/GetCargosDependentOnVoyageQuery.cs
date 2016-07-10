using CQRS.Domain.Models.VoyageModel;
using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Queries
{
    public class GetCargosDependentOnVoyageQuery : IQuery<IReadOnlyCollection<Cargo>>
    {
        public GetCargosDependentOnVoyageQuery(VoyageId voyageId)
        {
            VoyageId = voyageId;
        }

        public VoyageId VoyageId { get; }
    }
}