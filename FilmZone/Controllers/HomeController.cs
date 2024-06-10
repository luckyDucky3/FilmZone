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

        private IFilmService filmService;
        
        public HomeController(IFilmService filmService, ILogger<HomeController> logger)
        {
            _logger = logger;
            this.filmService = filmService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var response1 = await filmService.GetFilms();
            //int count = response1.Data.Count();
            //await filmService.DeleteFilm(12);
            //Film film8 = new Film()
            //{
            //    Name = "Остров проклятых",
            //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/348fd1f4-3f63-4c77-80da-8f0bce99fd83/1920x",
            //    Description =
            //        "Два американских судебных пристава отправляются на один из островов в штате Массачусетс, чтобы расследовать исчезновение пациентки клиники для умалишенных преступников. При проведении расследования им придется столкнуться с паутиной лжи, обрушившимся ураганом и смертельным бунтом обитателей клиники.",
            //    ReleaseFilmDate = 2009,
            //    Type = TypeFilm.Thriller,
            //    Director = "Мартин Скорсезе",
            //    FilmOrSerial = FilmOrSerial.Film,
            //    Preview = @"https://www.youtube.com/embed/_l7R9Rz5URw?si=fy096UnsB6qGeqYi"
            //};
            //film8.Links.Add(@"https://my.mail.ru/mail/vm_gluschenko/video/56039/243400.html");
            //film8.Price.Add("Бесплатно");
            //film8.Advertisement.Add("Без рекламы");
            //film8.Links.Add(@"https://www.kinopoisk.ru/film/397667/");
            //film8.Price.Add("Платно");
            //film8.Advertisement.Add("Без рекламы");
            //var resp1 = await filmService.UpdateFilm("Остров проклятых", film8);
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
