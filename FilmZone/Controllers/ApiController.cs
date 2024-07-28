using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmZone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        [HttpPost("heart")]
        public IActionResult AddFilmToFavourites()
        {
            if (HttpContext.Session.Get("_Name").Any())
            {

            }
            return Ok(new { success = true });
        }
    }
}
