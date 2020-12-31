using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfxApi.Controllers.v2
{
    [ApiController]
    [ApiVersion(Program.API_V_2_0, Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    public class SaveDataController
    {
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task Post([FromBody] string[] filesContent)
        {
            throw new NotImplementedException();
        }
    }
}
