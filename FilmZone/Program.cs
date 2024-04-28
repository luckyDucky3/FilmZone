using FilmZone.DAL;
using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Service.Implementations;
using FilmZone.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=filmdb;Username=postgres;Password=100506Ki"));
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllersWithViews();

//using (ApplicationDbContext db = new ApplicationDbContext()) //добавление данных
//{
//    Film film1 = new Film()
//    {
//        Name = "Облачный атлас",
//        Description =
//            "Цепочка перерождений связывает героев из разных эпох. Фантастический блокбастер с Томом Хэнксом и Холли Берри о том, " +
//            "как чья-то сохранившаяся на бумаге история, попадает в руки незнакомому человеку и совершенно завладевает его мыслями, " +
//            "продлевая себе жизнь в чужой памяти.",
//        ReleaseFilmDate = 2012,
//        Type = TypeFilm.Fantasy,
//        Director = "Лана и Эндрю Вачовски и Том Тыквер",
//        Preview = "https://youtu.be/K2VtiZSvwuo",
//    };
//    film1.Links.Add("https://vk.com/video-110645251_456240683");
//    film1.Price.Add("Бесплатно");
//    film1.Advertisement.Add("C рекламой");
//    film1.Links.Add("https://rutube.ru/video/71fa0ae2c405383724e143f6fee38330/?t=1");
//    film1.Price.Add("Бесплатно");
//    film1.Advertisement.Add("C рекламой");
    //Film film2 = new Film()
    //{
    //    Name = "Интерстеллар",
    //    Description = "Когда засуха, пыльные бури и вымирание растений приводят человечество к продовольственному кризису,коллектив исследователей" +
    //                      "и учёных отправляется сквозь червоточину (которая предположительно соединяетобласти пространства-времени через большое" +
    //                      "расстояние) в путешествие, чтобы превзойти прежние ограничения для космических путешествий человека и найти планету с" +
    //                      "подходящими для человечества условиями.",
    //    ReleaseFilmDate = 1979,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Кристофер Нолан",
    //    Preview = "https://www.youtube.com/embed/qcPfI0y7wRU",
    //    LinkF = "https://interstellar-lordfilm.ru/",
    //    LinkS = "https://kion.ru/video/movie/490694316"
    //};
    //Film film3 = new Film()
    //{
    //    Name = "Матрица: революция",
    //    Description = "Пока армия машин пытается уничтожить Зион, его жители из последних сил держат оборону",
    //    ReleaseFilmDate = 2003,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Сёстры Вачовски",
    //    Preview = "https://www.youtube.com/embed/KjKQGoZDiq4",
    //    LinkF = "https://vk.com/video-127060916_456239110",
    //    LinkS = "https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/?t=5"
    //};
    //    db.Film.Add(film1);
    //    db.SaveChanges();
    //}


    var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Search",
    pattern: "Home/Search/{searchField}");
app.Run();
