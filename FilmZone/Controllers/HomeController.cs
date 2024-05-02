using FilmZone.Domain.Enum;
using FilmZone.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FilmZone.Service.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Index()
        {
            int i = 1;
            int countFilm = 0, countSerial = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (i < 16)
            {
                var response = await filmService.GetFilm(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if (response.Data.FilmOrSerial == FilmOrSerial.Film && countFilm < 8)
                    {
                        countFilm++;
                        ListOfFilm.Add(response.Data);
                        i++;
                    }
                    else if(response.Data.FilmOrSerial == FilmOrSerial.Serial && countSerial < 8)
                    {
                        countSerial++;
                        ListOfFilm.Add(response.Data);
                        i++;
                    }
                }
                else if(response.StatusCode == Domain.Enum.StatusCode.InternalServerError 
                        || response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    break;
                }
            }

            return View(ListOfFilm);
        }
        [HttpGet]
        public async Task<IActionResult> Films(int countFMax)
        {
            int i = 1;
            int countF = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while(countF < countFMax)
            {
                var response = await filmService.GetFilm(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Film)
                {
                    ListOfFilm.Add(response.Data);
                    countF++;
                }
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "Конец";
                    break;
                }
                i++;
            }

            return View(ListOfFilm);
        }
        [HttpGet]
        public IActionResult Interstellar()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Serials()
        {
            int i = 1;
            int countS = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (countS < 4)
            {
                var response = await filmService.GetFilm(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Serial)
                {
                    ListOfFilm.Add(response.Data);
                    countS++;
                }
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    break;
                }
                i++;
            }

            return View(ListOfFilm);
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
        public async Task<IActionResult> SearchByName(string searchField)
        {
            var response = await filmService.GetFilmByName(searchField);
            if (response.StatusCode == Domain.Enum.StatusCode.OK ||
                response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "К сожалению такой фильм или сериал не найден :((";
                    return View("SearchFilm", null);
                }
                else
                {
                    FilmViewModel film = TransformToFilmViewModel(response.Data);
                    return View("SearchFilm", film);
                }
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> SearchById(int Id)
        {
            var response = await filmService.GetFilm(Id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK ||
                response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "К сожалению такой фильм или сериал не найден :((";
                    return RedirectToAction("SearchFilm", null);
                }
                else
                {
                    FilmViewModel film = TransformToFilmViewModel(response.Data);
                    return View("SearchFilm", film);
                }
            }
            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View();
        }


        private FilmViewModel TransformToFilmViewModel(ref readonly Film _film)
        {
            FilmViewModel film = new FilmViewModel()
            {
                Description = _film.Description,
                Id = _film.Id,
                PathToImage = _film.PathToImage,
                FilmOrSerial = _film.FilmOrSerial,
                Name = _film.Name,
                ReleaseFilmDate = _film.ReleaseFilmDate,
                Type = _film.Type,
                Director = _film.Director,
                Preview = _film.Preview,
                Links = new List<string>(_film.Links),
                Price = new List<string>(_film.Price),
                Advertisement = new List<string>(_film.Advertisement)
            };
            
            return film;
        }
    }
}
