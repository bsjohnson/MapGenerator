using Microsoft.AspNetCore.Mvc;

namespace Dungeosis.WebApp.Controllers
{
    [Route("api/map")]
    [ApiController]
    [Produces("application/json")]
    public class MapGeneratorController : Controller
    {

        [HttpGet]
        [Route("generate")]
        public IActionResult Generate()
        {
            return Json(new MapGenerator().Generate());
        }
    }
}