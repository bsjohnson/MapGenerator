using Dungeosis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dungeosis.WebApp.Controllers
{
    [Route("api/map")]
    [ApiController]
    public class MapGeneratorController : Controller {

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate() {
            Map map = new MapGenerator().Generate();
            
            return Content(map.GetGridAsString());
        }
    }
}