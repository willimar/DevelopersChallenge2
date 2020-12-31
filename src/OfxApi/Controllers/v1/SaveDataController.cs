using FinancialMediator;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfxApi.Controllers.v1
{
    [EnableCors(Program.AllowSpecificOrigins)]
    [ApiController]
    [ApiVersion(Program.API_V_1_0, Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Produces("application/json")]
    public class SaveDataController : Controller
    {
        private readonly SaveOfxData _saveOfxData;

        public SaveDataController(SaveOfxData saveOfxData)
        {
            this._saveOfxData = saveOfxData;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task Post([FromBody] string[] filesContent)
        {
            await this._saveOfxData.Save(filesContent);
        }

        
    }
}
