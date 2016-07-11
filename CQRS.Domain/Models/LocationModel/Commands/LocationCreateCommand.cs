using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.LocationModel.Commands
{
    public class LocationCreateCommand : Command<LocationAggregate, LocationId>
    {
        public string Name { get; }

        public LocationCreateCommand(LocationId aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }
    }

    public class LocationCreateCommandHandler : CommandHandler<LocationAggregate, LocationId, LocationCreateCommand>
    {
        public override Task ExecuteAsync(LocationAggregate aggregate, LocationCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.Name);
            return Task.FromResult(0);
        }
    }
}