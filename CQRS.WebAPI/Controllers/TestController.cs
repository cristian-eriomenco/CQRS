using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CQRS.WebAPI.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }
    }
}