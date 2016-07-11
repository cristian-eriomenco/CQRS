using CQRS.Domain.Models.LocationModel;
using CQRS.Domain.Models.LocationModel.Commands;
using CQRS.Domain.Models.LocationModel.Queries;
using CQRS.WebAPI.DTO;
using EventFlow;
using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace CQRS.WebAPI.Controllers
{
    [RoutePrefix("api/locations")]
    public class LocationController : ApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public LocationController(
            ICommandBus commandBus,
            IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> Post([FromBody]CreateLocationDTO location)
        {
            var locationCreateCommand = new LocationCreateCommand(new LocationId(location.UNCode), location.Name);

            await _commandBus.PublishAsync(locationCreateCommand, CancellationToken.None).ConfigureAwait(false);

            //var link = Url.Link("LocationCreated", new { locationId = locationCreateCommand.AggregateId.Value.ToString() });
            //return Created(new Uri(link), new { Id = locationCreateCommand.AggregateId, Name = location.Name });

            return Ok();
        }

        [HttpGet]
        [Route]
        [ResponseType(typeof(IReadOnlyCollection<Location>))]
        public async Task<IHttpActionResult> Get()
        {
            var query = new GetLocationsQuery();

            var result = await _queryProcessor.ProcessAsync(query, CancellationToken.None).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet]
        [Route("{locationId}", Name = "LocationCreated")]
        public async Task<IHttpActionResult> GetById([FromUri]string locationId)
        {
            var query = new GetLocationsQuery(new LocationId(locationId));

            var result = await _queryProcessor.ProcessAsync(query, CancellationToken.None).ConfigureAwait(false);

            return Ok(result);
        }
    }
}