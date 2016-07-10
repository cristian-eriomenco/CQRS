using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.Entities
{
    public class CarrierMovementId : Identity<CarrierMovementId>
    {
        public CarrierMovementId(string value) : base(value)
        {
        }
    }
}