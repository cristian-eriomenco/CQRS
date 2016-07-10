using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class VoyageId : SingleValueObject<string>, IIdentity
    {
        public VoyageId(string value) : base(value)
        {
        }
    }
}