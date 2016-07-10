using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel
{
    public class Voyage : Entity<VoyageId>
    {
        public Voyage(
            VoyageId id,
            Schedule schedule)
            : base(id)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }
}