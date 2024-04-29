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

using (ApplicationDbContext db = new ApplicationDbContext()) //добавление данных
{
    //Film film1 = new Film()
    //{
    //    Name = "Облачный атлас",
    //    PathToImage = @"~/img/cloud.png",
    //    Description =
    //        "Цепочка перерождений связывает героев из разных эпох. Фантастический блокбастер с Томом Хэнксом и Холли Берри о том, " +
    //        "как чья-то сохранившаяся на бумаге история, попадает в руки незнакомому человеку и совершенно завладевает его мыслями, " +
    //        "продлевая себе жизнь в чужой памяти.",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Лана и Эндрю Вачовски и Том Тыквер",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/K2VtiZSvwuo?si=XUF0ThhXcKrr9Gr3"
    //};
    //film1.Links.Add(@"https://vk.com/video-110645251_456240683");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("C рекламой");
    //film1.Links.Add(@"https://rutube.ru/video/71fa0ae2c405383724e143f6fee38330/?t=1");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("C рекламой");
    //Film film2 = new Film()
    //{
    //    Name = "Интерстеллар",
    //    PathToImage = @"~/img/inter.png",
    //    Description = "Когда засуха, пыльные бури и вымирание растений приводят человечество к продовольственному кризису,коллектив исследователей" +
    //                      "и учёных отправляется сквозь червоточину (которая предположительно соединяетобласти пространства-времени через большое" +
    //                      "расстояние) в путешествие, чтобы превзойти прежние ограничения для космических путешествий человека и найти планету с" +
    //                      "подходящими для человечества условиями.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Кристофер Нолан",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/qcPfI0y7wRU?si=wip5_nml6yQBHsJO",
    //};
    //film2.Links.Add(@"https://vk.com/video-213377389_456241538");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("C рекламой");
    //film2.Links.Add(@"https://my.mail.ru/mail/ppalekss/video/5/160.html");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://lordfilmi.org/982-film-interstellar.html");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("C рекламой");
    //film2.Links.Add(@"https://interstellar-lordfilms.ru/");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("C рекламой");
    //Film film3 = new Film()
    //{
    //    Name = "Матрица: революция",
    //    PathToImage = @"~/img/matrix.png",
    //    Description = "Пока армия машин пытается уничтожить Зион, его жители из последних сил держат оборону",
    //    ReleaseFilmDate = 2003,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Лана Вачовски, Лилли Вачовски",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/KjKQGoZDiq4",
    //};
    //film3.Links.Add(@"https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/?t=5");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("C рекламой");
    //film3.Links.Add(@"https://my.mail.ru/mail/votvamby68/video/_myvideo/3871.html?from=videoplayer");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("Без рекламы");
    //film3.Links.Add(@"https://vk.com/video-127060916_456239110");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("C рекламой");
    //film3.Links.Add(@"https://matrix-lordfilm.com/film/matritsa-3-revolyutsiya");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("C рекламой");
    //Film film4 = new Film()
    //{
    //    Name = "Безумный Макс",
    //    PathToImage = @"~/img/max.png",
    //    Description = "В недалёком будущем после крупной катастрофы вся жизнь сосредоточилась вдоль бесчисленных магистралей. Банда байкеров, желая " +
    //                  "рассчитаться за убитого товарища, преследует молодого полицейского Макса. Жертвой их мести становится лучший друг Макса, и теперь " +
    //                  "эта же участь грозит самому Максу и его семье.",
    //    ReleaseFilmDate = 2015,
    //    Type = TypeFilm.Military,
    //    Director = "Лана Вачовски, Лилли Вачовски",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/-3ZoAp6owdk?si=OU1cF9sMIrz395p3",
    //};
    //film4.Links.Add(@"https://vk.com/video-176294899_456240989");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("C рекламой");
    //film4.Links.Add(@"https://ok.ru/video/1792787548709");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"https://hdf4im.kinolord6.pics/7267-bezumnyj-maks-1979.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("C рекламой");
    //film4.Links.Add(@"https://my.mail.ru/mail/asps11/video/297/8809.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");
    //Film film5 = new Film()
    //{
    //    Name = "Во все тяжкие",
    //    PathToImage = @"~/img/breakingbad.png",
    //    Description = "Школьный учитель химии Уолтер Уайт узнаёт, что болен раком лёгких. " +
    //                  "Учитывая сложное финансовое состояние дел семьи, а также перспективы, Уолтер решает заняться изготовлением метамфетамина. " +
    //                  "Для этого он привлекает своего бывшего ученика Джесси Пинкмана, когда-то исключённого из школы при активном содействии Уайта. " +
    //                  "Пинкман сам занимался варкой мета, но накануне, в ходе рейда УБН, он лишился подельника и лаборатории.",
    //    ReleaseFilmDate = 2008,
    //    Type = TypeFilm.Criminal,
    //    Director = "Мишель Макларен, Адам Бернштейн, Винс Гиллиган",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/OpcX_Q0PGM4?si=XwXDXAfWWpq05ZSh",
    //};
    //film5.Links.Add(@"https://breakingbad.smotret-online.ru/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("C рекламой");
    //film5.Links.Add(@"https://rutube.ru/plst/362164/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("C рекламой");
    //film5.Links.Add(@"https://breaking.smotrim-smotrim.ru/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("C рекламой");
    //film5.Links.Add(@"https://9lordserial.art/29-vo-vse-tiazkie-5-sezon-v186.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("C рекламой");
    //Film film6 = new Film()
    //{
    //    Name = "Ходячие мертвецы",
    //    PathToImage = @"~/img/dead.png",
    //    Description = "Зомби-эпидемия захлестнула планету. Шериф Рик Граймс путешествует с семьей и небольшой группой выживших в поисках безопасного " +
    //                  "места. Но постоянный страх смерти каждый день приносит тяжелые потери, заставляя товарищей по несчастью чувствовать глубины челов" +
    //                  "еческой жестокости. Рик пытается спасти близких и понимает, что всепоглощающий страх людей может быть опаснее ходячих мертвецов.",
    //    ReleaseFilmDate = 2010,
    //    Type = TypeFilm.Horrors,
    //    Director = "Грег Никотеро, Майкл Е. Сатраземис, Дэвид Бойд",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/0wz2uCXg9aw?si=aj1ToUUbFaWrcnkY",
    //};
    //film6.Links.Add(@"https://lordserialk7.top/zarubezhnyi/34-hodjachie-mertvecy_v3.html");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("C рекламой");
    //film6.Links.Add(@"https://vk.com/video-80021931_456239833");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("C рекламой");
    //film6.Links.Add(@"https://18lordserial.xyz/462-xodiacie-mertvecy-sv-29.html");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("C рекламой");
    //film6.Links.Add(@"https://walking-dead.homes/?utm_referrer=https%3A%2F%2Fyandex.ru%2F");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("C рекламой");
    //db.Film.AddRange(film1, film2, film3, film4, film5, film6);
    //db.SaveChanges();
}


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
