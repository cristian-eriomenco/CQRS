using CQRS.Domain.Applications;
using CQRS.Domain.Models.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CQRS.WebAPI.Controllers
{
    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        private readonly IScheduleApplicationService _scheduleService;

        public ScheduleController(IScheduleApplicationService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        [Route("{voyageId}/delay")]
        public async Task<IHttpActionResult> DelaySchedule([FromUri]string voyageId, [FromBody] TimeSpan delay)
        {
            await _scheduleService.DelayScheduleAsync(new VoyageId(voyageId), delay, CancellationToken.None).ConfigureAwait(false);
            return Ok();
        }
    }
}