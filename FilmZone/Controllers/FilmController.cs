using FilmZone.Domain.Enum;
using FilmZone.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FilmZone.Service.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace FilmZone.Controllers
{
    public class FilmController : BaseController
    {
        private int testValue = 0;
        
        public FilmController(IFilmService filmService, ILogger<FilmController> logger, 
            ISiteFeedbackService feedbackService, IHttpContextAccessor httpcontextAccessor) 
            : base(logger, filmService, httpcontextAccessor, feedbackService) 
        { }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            int i = 1;
            int countFilm = 0, countSerial = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (countSerial + countFilm < 16)
            {
                var response = await filmService.GetFilmById(i);
                i++;
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    if (response.Data.FilmOrSerial == FilmOrSerial.Film && countFilm < 8)
                    {
                        countFilm++;
                        ListOfFilm.Add(response.Data);
                    }
                    else if(response.Data.FilmOrSerial == FilmOrSerial.Serial && countSerial < 8)
                    {
                        countSerial++;
                        ListOfFilm.Add(response.Data);
                    }
                }
                if(response.StatusCode == Domain.Enum.StatusCode.InternalServerError || i > 60)
                {
                    break;
                }
            }
            testValue++;
            return View(ListOfFilm);
        }
        [HttpGet]
        public IActionResult Films()
        {
            return View();
        }

        public async Task<IActionResult> AjaxSearchFilms(int currentId)
        {
            int countF = 0;
            ViewBag.End = false;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (countF < 4)
            {
                var response = await filmService.GetFilmById(currentId);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Film)
                {
                    ListOfFilm.Add(response.Data);
                    countF++;
                }
                if (currentId == 100)
                {
                    ViewBag.End = true;
                    break;
                }
                currentId++;
            }
            ViewBag.id = currentId;
            return PartialView(ListOfFilm);
        }

        [HttpGet]
        public IActionResult Serials()
        {
            return View();
        }

        public async Task<IActionResult> AjaxSearchSerials(int currentId)
        {
            int countF = 0;
            ViewBag.End = false;
            List<FilmViewModel> ListOfSerials = new List<FilmViewModel>();
            while (countF < 4)
            {
                var response = await filmService.GetFilmById(currentId);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Serial)
                {
                    ListOfSerials.Add(response.Data);
                    countF++;
                }
                if (currentId == 100)
                {
                    ViewBag.End = true;
                    break;
                }
                currentId++;
            }
            ViewBag.id = currentId;
            return PartialView(ListOfSerials);
        }

        [HttpGet]
        public async Task<IActionResult> Rating()
        {
            var response = await filmService.GetFilmsInOrderByRating(10);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error");
        }
        [HttpGet]
        public IActionResult Kontakts()
        {
            return View(false);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByName(string searchField)
        {
            var response = await filmService.GetFilmByName(searchField);
            if (response.StatusCode == Domain.Enum.StatusCode.OK ||
                response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "  сожалению такой фильм или сериал не найден :((";
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
            var response = await filmService.GetFilmById(Id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK ||
                response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
                {
                    ViewData["Message"] = "  сожалению такой фильм или сериал не найден :((";
                    return RedirectToAction("SearchFilm", "Film", null);
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
        public async Task<IActionResult> SearchByType(TypeFilm type)
        {
            int id = 1;
            List<Film> ListOfFilm = new List<Film>();
            IBaseResponse<List<Film>> response;
            response = await filmService.GetFilmByType(type);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ListOfFilm.AddRange(response.Data);
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.FilmNotFound)
            {
                ListOfFilm = null;
            }
            else 
            {
                return RedirectToAction("Error");
            }
            ViewBag.Type = type;
            return View(ListOfFilm);
        }

        public async Task<IActionResult> SendFeedback(string Text)
        {
            SiteFeedback fb = new SiteFeedback();
            fb.Name = httpcontextAccessor.HttpContext.Session.GetString("_Name");
            fb.Text = Text;
            var response = await feedbackService.CreateFeedback(fb);
            return View("Kontakts", response.Data);
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
                Advertisement = new List<string>(_film.Advertisement),
                Rating = _film.Rating
            };
            
            return film;
        }
    }
}
