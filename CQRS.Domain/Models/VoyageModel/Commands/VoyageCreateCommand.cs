using CQRS.Domain.Models.VoyageModel.ValueObjects;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.VoyageModel.Commands
{
    public class VoyageCreateCommand : Command<VoyageAggregate, VoyageId>
    {
        public VoyageCreateCommand(
            VoyageId aggregateId,
            Schedule schedule)
            : base(aggregateId)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }

    public class VoyageCreateCommandHandler : CommandHandler<VoyageAggregate, VoyageId, VoyageCreateCommand>
    {
        public override Task ExecuteAsync(VoyageAggregate aggregate, VoyageCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.Schedule);
            return Task.FromResult(0);
        }
    }
}