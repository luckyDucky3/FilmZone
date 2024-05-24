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
            //await filmService.DeleteFilm(12);
            //FilmViewModel film1 = new FilmViewModel()
            //{
            //    Name = "�������",
            //    PathToImage = @"https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.kino-teatr.ru%2Fkino%2Fmovie%2Fros%2F145171%2Ftitr%2F&psig=AOvVaw2DNrc2jEwsqyz1q4yGGrCa&ust=1716637671406000&source=images&cd=vfe&opi=89978449&ved=0CBAQjRxqFwoTCPiOuumbpoYDFQAAAAAdAAAAABAE",
            //    Description =
            //        "�������� ���� ���������� � ��������� ������� ������� � ������� �������. �� �������, ��� ������������ ������ ��� �������� ������ ���� " +
            //        "�������� � ��� ������ ���� � ��������� ���� �����. ���� ����������� ���������� �������� � ���������, �� ������� ����������� ������ �� �����" +
            //        ", ����������� � �������� ��� ���� ������� ������ �� ���� �������, �� ���� ��������� ����������� ��������: ���������� ��, ������� ��� ����," +
            //        " ��������� ����������� �� ���� ��������. �������� ������ ����������, ���� ���� �� ��� ��������� �� ������� ����� �������������.",
            //    ReleaseFilmDate = 2020,
            //    Type = TypeFilm.Drama,
            //    Director = "������� �����, ����� ������������",
            //    FilmOrSerial = FilmOrSerial.Serial,
            //    Preview = @"https://www.youtube.com/embed/L5955NbSKRM?si=OIEeKmfM6jirrc4X"
            //};
            //film1.Links.Add(@"https://hd.kinopoisk.ru/film/4f3c027cbce13606b74124a9d5b140c7");
            //film1.Price.Add("������");
            //film1.Advertisement.Add("��� �������");
            //film1.Links.Add(@"https://trigger.lordfilm.ph/");
            //film1.Price.Add("���������");
            //film1.Advertisement.Add("C ��������");
            //film1.Links.Add(@"https://pro.hdprolord.run/325-film-djavol-nosit-prada.html");
            //film1.Price.Add("���������");
            //film1.Advertisement.Add("C ��������");
            //var resp1 = await filmService.UpdateFilm("�������", film1);
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
                if (i == 61)
                {
                    ViewData["Message"] = "�����";
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
                if (i == 61)
                {
                    ViewData["Message"] = "�����";
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
                    ViewData["Message"] = "� ��������� ����� ����� ��� ������ �� ������ :((";
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
                    ViewData["Message"] = "� ��������� ����� ����� ��� ������ �� ������ :((";
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
