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

        private IFilmService filmService;
        
        public HomeController(IFilmService filmService)
        {
            //_logger = logger;
            this.filmService = filmService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var response1 = await filmService.GetFilms();
            //int count = response1.Data.Count();
            //FilmViewModel film1 = new FilmViewModel()
            //{
            //    Name = "Триггер",
            //    PathToImage = @"https://kupi-vse.ru/wa-data/public/shop/products/23/61/26123/images/30383/30383.750x0.jpg",
            //    Description =
            //        "Психолог Артём Стрелецкий — сторонник шоковой терапии в лечении больных. Он считает, что единственный способ для человека решить свои " +
            //        "проблемы — это понять себя и перестать себе врать. Если большинство психологов нянчатся с клиентами, по полгода выслушивают жалобы на жизнь" +
            //        ", сочувствуют и получают при этом немалые деньги за цикл сеансов, то Артём постоянно провоцирует клиентов: оскорбляет их, смеется над ними," +
            //        " намеренно выталкивает из зоны комфорта. Практика Артема процветает, пока один из его пациентов не кончает жизнь самоубийством.",
            //    ReleaseFilmDate = 2020,
            //    Type = TypeFilm.Drama,
            //    Director = "Дмитрий Тюрин, Игорь Твердохлебов",
            //    FilmOrSerial = FilmOrSerial.Serial,
            //    Preview = @"https://www.youtube.com/embed/L5955NbSKRM?si=OIEeKmfM6jirrc4X"
            //};
            //film1.Links.Add(@"https://hd.kinopoisk.ru/film/4f3c027cbce13606b74124a9d5b140c7");
            //film1.Price.Add("Платно");
            //film1.Advertisement.Add("Без рекламы");
            //film1.Links.Add(@"https://trigger.lordfilm.ph/");
            //film1.Price.Add("Бесплатно");
            //film1.Advertisement.Add("C рекламой");
            //film1.Links.Add(@"https://pro.hdprolord.run/325-film-djavol-nosit-prada.html");
            //film1.Price.Add("Бесплатно");
            //film1.Advertisement.Add("C рекламой");
            //var resp1 = await filmService.Edit(12, film1);
            int i = 1;
            int countFilm = 0, countSerial = 0;
            List<FilmViewModel> ListOfFilm = new List<FilmViewModel>();
            while (i < 16)
            {
                var response = await filmService.GetFilmById(i);
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
                    i++;
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
                var response = await filmService.GetFilmById(i);
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
