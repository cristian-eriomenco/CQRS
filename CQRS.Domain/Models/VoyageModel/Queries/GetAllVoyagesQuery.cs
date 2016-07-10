using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.Queries
{
    public class GetAllVoyagesQuery : IQuery<IReadOnlyCollection<Voyage>>
    {
    }
}