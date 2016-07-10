﻿using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.Queries
{
    public class GetVoyagesQuery : IQuery<IReadOnlyCollection<Voyage>>
    {
        public GetVoyagesQuery(
            IEnumerable<VoyageId> voyageIds)
        {
            VoyageIds = voyageIds.ToList();
        }

        public IReadOnlyCollection<VoyageId> VoyageIds { get; }
    }
}