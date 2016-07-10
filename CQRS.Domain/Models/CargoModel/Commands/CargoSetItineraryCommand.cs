﻿using CQRS.Domain.Models.CargoModel.ValueObjects;
using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Commands
{
    public class CargoSetItineraryCommand : Command<CargoAggregate, CargoId>
    {
        public CargoSetItineraryCommand(
            CargoId aggregateId,
            Itinerary itinerary)
            : base(aggregateId)
        {
            Itinerary = itinerary;
        }

        public Itinerary Itinerary { get; }
    }

    public class CargoSetItineraryCommandHandler : CommandHandler<CargoAggregate, CargoId, CargoSetItineraryCommand>
    {
        public override Task ExecuteAsync(CargoAggregate aggregate, CargoSetItineraryCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetItinerary(command.Itinerary);
            return Task.FromResult(0);
        }
    }
}