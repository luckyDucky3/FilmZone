using FilmZone.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FilmZone.Service.Interfaces;

namespace FilmZone.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        IFilmService filmService;

        public HomeController(IFilmService filmService)
        {
            //_logger = logger;
            this.filmService = filmService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Films()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Interstellar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Serials()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Rating()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Kontakts()
        {
            return View();
        }
        //[HttpPost]
        [HttpGet]
        public async Task<IActionResult> Search(string searchField)
        {
            var response = await filmService.GetFilmByName(searchField);
            if (response.StatusCode == Domain.Enum.StatusCode.OK ||
                response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "  сожалению такой фильм или сериал не найден :((";
                    return View(null);
                }
                else
                {
                    FilmViewModel film = new FilmViewModel()
                    {
                        Description = response.Data.Description,
                        Id = response.Data.Id,
                        Name = response.Data.Name,
                        ReleaseFilmDate = response.Data.ReleaseFilmDate,
                        Type = response.Data.Type,
                        Director = response.Data.Director,
                        Preview = response.Data.Preview,
                        LinkF = response.Data.LinkF,
                        LinkS = response.Data.LinkS
                    };
                    return View(film);
                }
            }

            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
