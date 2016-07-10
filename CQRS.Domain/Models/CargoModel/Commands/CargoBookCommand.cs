using CQRS.Domain.Models.CargoModel.ValueObjects;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Commands
{
    public class CargoBookCommand : Command<CargoAggregate, CargoId>
    {
        public CargoBookCommand(
            CargoId aggregateId,
            Route route)
            : base(aggregateId)
        {
            Route = route;
        }

        public Route Route { get; }
    }

    public class CargoBookCommandHandler : CommandHandler<CargoAggregate, CargoId, CargoBookCommand>
    {
        public override Task ExecuteAsync(CargoAggregate aggregate, CargoBookCommand command, CancellationToken cancellationToken)
        {
            aggregate.Book(command.Route);
            return Task.FromResult(0);
        }
    }
}