using CQRS.Domain.Models.CargoModel.Commands;
using CQRS.Domain.Models.CargoModel.Queries;
using CQRS.Domain.Services;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Exceptions;
using EventFlow.Jobs;
using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Domain.Models.CargoModel.Jobs
{
    public class VerifyCargoItineraryJob : IJob
    {
        public VerifyCargoItineraryJob(
            CargoId cargoId)
        {
            CargoId = cargoId;
        }

        public CargoId CargoId { get; }

        public async Task ExecuteAsync(IResolver resolver, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var updateItineraryService = resolver.Resolve<IUpdateItineraryService>();
            var commandBus = resolver.Resolve<ICommandBus>();
            var routingService = resolver.Resolve<IRoutingService>();

            var cargo = (await queryProcessor.ProcessAsync(new GetCargosQuery(CargoId), cancellationToken).ConfigureAwait(false)).Single();
            var updatedItinerary = await updateItineraryService.UpdateItineraryAsync(cargo.Itinerary, cancellationToken).ConfigureAwait(false);

            if (cargo.Route.Specification().IsSatisfiedBy(updatedItinerary))
            {
                await commandBus.PublishAsync(new CargoSetItineraryCommand(cargo.Id, updatedItinerary), cancellationToken).ConfigureAwait(false);
                return;
            }

            var newItineraries = await routingService.CalculateItinerariesAsync(cargo.Route, cancellationToken).ConfigureAwait(false);

            var newItinerary = newItineraries.FirstOrDefault();
            if (newItinerary == null)
            {
                // TODO: Tell domain that a new itinerary could not be found
                throw DomainError.With("Could not find itinerary");
            }

            await commandBus.PublishAsync(new CargoSetItineraryCommand(cargo.Id, newItinerary), cancellationToken).ConfigureAwait(false);
        }
    }
}