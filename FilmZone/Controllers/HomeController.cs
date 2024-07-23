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
        private readonly ILogger<HomeController> _logger;
        private int testValue = 0;
        private IFilmService filmService;
        private IFeedbackService feedbackService;
        
        public HomeController(IFilmService filmService, ILogger<HomeController> logger, IFeedbackService feedbackService)
        {
            _logger = logger;
            this.filmService = filmService;
            this.feedbackService = feedbackService;
        }
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
        public async Task<IActionResult> Films(int countFMax)
        {
            int i = 1;
            int countF = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while(countF < countFMax)
            {
                var response = await filmService.GetFilmById(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Film)
                {
                    ListOfFilm.Add(response.Data);
                    countF++;
                }
                if (i == 100)
                {
                    ViewData["Message"] = "Конец";
                    break;
                }
                i++;
            }

            return View(ListOfFilm);
        }
        [HttpGet]
        public async Task<IActionResult> Serials(int countSMax)
        {
            int i = 1;
            int countF = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (countF < countSMax)
            {
                var response = await filmService.GetFilmById(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Serial)
                {
                    ListOfFilm.Add(response.Data);
                    countF++;
                }
                if (i == 100)
                {
                    ViewData["Message"] = "Конец";
                    break;
                }
                i++;
            }

            return View(ListOfFilm);
        }
        [HttpGet]
        public async Task<IActionResult> Rating()
        {
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            int countF = 1;
            for (int i = 0; countF < 5; i++)
            {
                var response = await filmService.GetFilmById(i);
                if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.FilmOrSerial == FilmOrSerial.Serial)
                {
                    ListOfFilm.Add(response.Data);
                    countF++;
                }
            }
            
            return View(ListOfFilm);
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
            var response = await filmService.GetFilmById(Id);
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

        [HttpGet]
        public async Task<IActionResult> SearchByType(TypeFilm type)
        {
            int id = 1;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            var response = await filmService.GetFilmById(id);
            id++;
            while (id < 100)
            {
                if(response.StatusCode == Domain.Enum.StatusCode.OK && response.Data.Type == type)
                {
                    ListOfFilm.Add(response.Data);
                }
                response = await filmService.GetFilmById(id);
                id++;
            }
            ViewData["Type"] = type;
            return View(ListOfFilm);
        }

        public async Task<IActionResult> SendFeedback(string Name, string Email, string Text)
        {
            Feedback fb = new Feedback();
            fb.Name = Name;
            fb.Email = Email;
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
                Advertisement = new List<string>(_film.Advertisement)
            };
            
            return film;
        }
    }
}
