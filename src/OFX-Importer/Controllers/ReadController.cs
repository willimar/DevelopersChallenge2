using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OFX_Importer.Controllers
{
    public class ReadController : Controller
    {
        public async Task<IActionResult> Index([FromServices] HttpClient httpClient)
        {
            var apiDomain = Environment.GetEnvironmentVariable("API_OFX_URL", EnvironmentVariableTarget.Machine);
            var url = $"{apiDomain}/api/v1/GetData/Banks";
            var result = await httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var banksJson = await result.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<int>>(banksJson);

                this.ViewData["API_DOMAIN"] = apiDomain;
                return View(model);
            }

            return View(new List<int>());
        }
    }
}
