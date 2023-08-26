using Microsoft.AspNetCore.Mvc;

namespace MapGenerator.WebApp.Controllers
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