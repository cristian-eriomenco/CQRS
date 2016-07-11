using CQRS.Domain.Applications;
using CQRS.Domain.Models.CargoModel.ValueObjects;
using CQRS.Domain.Models.LocationModel;
using CQRS.WebAPI.DTO;
using EventFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CQRS.WebAPI.Controllers
{
    public class CargoController : ApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly IBookingApplicationService _bookingService;

        public CargoController(
            ICommandBus commandBus,
            IBookingApplicationService bookingService)
        {
            _commandBus = commandBus;
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("book")]
        public async Task<IHttpActionResult> BookCargo([FromBody]CreateBookingDTO route)
        {
            var r = new Route(new LocationId(route.OriginId), new LocationId(route.DestinationId), route.DepartureDate, route.ArrivalDate);

            await _bookingService.BookCargoAsync(r, CancellationToken.None);

            return Ok();
        }
    }
}