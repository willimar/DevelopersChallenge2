using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OFX_Importer.Controllers
{
    public class ImportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendData([FromBody] string[] filesContent, [FromServices] HttpClient httpClient)
        {
            var apiDomain = Environment.GetEnvironmentVariable("API_OFX_URL", EnvironmentVariableTarget.Machine);
            var url = $"{apiDomain}/api/v1/SaveData/Post";
            var content = new StringContent(JsonConvert.SerializeObject(filesContent), Encoding.UTF8, "application/json");
            await httpClient.PostAsync(url, content);

            return StatusCode((int)HttpStatusCode.Accepted, "Data saved in database.");
        }
    }
}
