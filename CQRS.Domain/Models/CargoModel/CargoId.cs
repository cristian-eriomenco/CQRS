using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CargoId : Identity<CargoId>
    {
        public CargoId(string value) : base(value)
        {
        }
    }
}