using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.LocationModel
{
    public class Location : Entity<LocationId>
    {
        public Location(
            LocationId id,
            string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}