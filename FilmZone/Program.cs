using FilmZone.DAL;
using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Domain.ViewModels;
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllersWithViews();

using (ApplicationDbContext db = new ApplicationDbContext()) //добавление данных
{
    //Film viewModel = new Film()
    //{
    //    Name = "Облачный атлас",
    //    Description = "Цепочка перерождений связывает героев из разных эпох. Фантастический блокбастер с Томом Хэнксом и Холли Берри о том, как чья-то сохранившаяся на бумаге исто" +
    //                  "рия, попадает в руки незнакомому человеку и совершенно завладевает его мыслями, продлевая себе жизнь в чужой памяти.",
    //    PathToImage = "~/img/CloudAtlas.png",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Лана Вачовски, Том Тыквер, Лилли Вачовски",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = "https://www.youtube.com/embed/K2VtiZSvwuo?si=edMZkW70pSaRGQOl",
    //};
    //viewModel.Links.Add("https://www.kinopoisk.ru/film/464484/");
    //viewModel.Price.Add("Платно");
    //viewModel.Advertisement.Add("Без рекламы");
    //viewModel.Links.Add("https://okko.tv/movie/cloud-atlas");
    //viewModel.Price.Add("Платно");
    //viewModel.Advertisement.Add("Без рекламы");
    //viewModel.Links.Add("https://vk.com/video-220018529_456240240");
    //viewModel.Price.Add("Беслатно");
    //viewModel.Advertisement.Add("С рекламой");
    //viewModel.Links.Add("https://ru.lordsfilm.space/333-oblachnyj-atlas.html");
    //viewModel.Price.Add("Беслатно");
    //viewModel.Advertisement.Add("С рекламой");
    //Film film1 = new Film()
    //{
    //    Name = "Силиконовая долина",
    //    PathToImage = @"~/img/silicon.png",
    //    Description =
    //        "История о группе гиков, готовящих к запуску собственные стартапы в высокотехнологичном центре Сан-Франциско. Главные герои сериала бесплатно " +
    //        "проживают в доме местного миллионера, но взамен им придётся отдать по 10% прибыли от будущих проектов.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Comedy,
    //    Director = "Майк Джадж, Алек Берг, Джеми Бэббит",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/BbyByc47qno?si=Q1Bjzfw9p4mckGdt"
    //};
    //film1.Links.Add(@"https://hd.lordserialx.online/426-silikonovaja-dolina-2014.html");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("C рекламой");
    //film1.Links.Add(@"https://vk.com/video-182169551_456256578");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("C рекламой");
    //film1.Links.Add(@"https://silicon-valley-kubik.net/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("C рекламой");
    //film1.Links.Add(@"https://www.ivi.ru/watch/silikonovaya-dolina");
    //film1.Price.Add("Платно");
    //film1.Advertisement.Add("Без рекламы");
    //    Film film2 = new Film()
    //    {
    //        Name = "Секретные материалы",
    //        PathToImage = @"~/img/xfiles.png",
    //        Description = "Агентам ФБР Дане Скалли и Фоксу Малдеру поручают работу над проектом «Секретные материалы». Это архив нераскрытых дел, связанных" +
    //                      " с паранормальными явлениями. Малдер верит в пришельцев и пытается убедить скептика Скалли, что не всё поддаётся разумному объяснению." +
    //                      " Постепенно взаимное недоверие перерастает в дружбу и даже в более глубокое чувство.",
    //        ReleaseFilmDate = 1993,
    //        Type = TypeFilm.Fantasy,
    //        Director = "Ким Мэннерс, Роб Боумен, Дэвид Наттер",
    //        FilmOrSerial = FilmOrSerial.Serial,
    //        Preview = @"https://www.youtube.com/embed/Xcf44Nit7_A?si=-Qkq2w0K-a_Rmew0",
    //    };
    //    film2.Links.Add(@"https://rutube.ru/plst/351666/");
    //    film2.Price.Add("Бесплатно");
    //    film2.Advertisement.Add("C рекламой");
    //    film2.Links.Add(@"https://vk.com/video/playlist/-221125343_172");
    //    film2.Price.Add("Бесплатно");
    //    film2.Advertisement.Add("C рекламой");
    //    film2.Links.Add(@"https://xfiles.top/seasons?utm_referrer=https%3A%2F%2Fyandex.ru%2F");
    //    film2.Price.Add("Бесплатно");
    //    film2.Advertisement.Add("C рекламой");
    //    film2.Links.Add(@"https://9lordserial.art/289-sekretnye-materialy-11-sezon-v186.html");
    //    film2.Price.Add("Бесплатно");
    //    film2.Advertisement.Add("C рекламой");

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
    //Film film1 = new Film()
    //{
    //    Name = "Триггер",
    //    PathToImage = @"http://www.prdisk.ru/images/trigger_dvd_16_serijj._5_dvd_r_f3d_big.jpeg",
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
    //Film film2 = new Film()
    //{
    //    Name = "Вам и не снилось",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/2/25/1981_vam_i_ne_snilos.jpg/184px-1981_vam_i_ne_snilos.jpg",
    //    Description =
    //        "Взрослые пытаются помешать отношениям двух подростков. Великое, пронзительно нежное кино о первой любви",
    //    ReleaseFilmDate = 1980,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Илья Фрэз",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/aBiPSjBs7XI?si=EB3_WwXU03gEC-UX"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/45660/?utm_referrer=www.google.com");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://www.youtube.com/embed/K7nTbO8kMAo?si=j5JTv3uaMiHfdUoY");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://www.ivi.ru/watch/51623");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("Без рекламы");


    //Film film1 = new Film()
    //{
    //    Name = "Крестный отец",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/c11652e8-653b-47c1-8e72-1552399a775b/220x330",
    //    Description =
    //        "Криминальная сага, повествующая о нью-йоркской сицилийской мафиозной семье Корлеоне. Фильм охватывает период 1945-1955 годов. Глава семьи, Дон Вито Корлеоне, выдаёт замуж свою дочь. В это время со Второй мировой войны возвращается его любимый сын Майкл. Майкл, герой войны, гордость семьи, не выражает желания заняться жестоким семейным бизнесом. Дон Корлеоне ведёт дела по старым правилам, но наступают иные времена, и появляются люди, желающие изменить сложившиеся порядки. На Дона Корлеоне совершается покушение.",
    //    ReleaseFilmDate = 1972,
    //    Type = TypeFilm.Criminal,
    //    Director = "Фрэнсис Форд Коппола",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/ar1SHxgeZUc?si=A79z5KZMg9cbeLbO"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/39568c110b232ad19985fb3a339f486d/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://godfather-film.ru/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("С рекламой");
    //film1.Links.Add(@"https://www.culture.ru/live/movies/352/a-zori-zdes-tikhie");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //Film film2 = new Film()
    //{
    //    Name = "1+1",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b9/Intouchables.jpg",
    //    Description =
    //        "Пострадав в результате несчастного случая, богатый аристократ Филипп нанимает в помощники человека, который менее всего подходит для этой работы, – молодого жителя предместья Дрисса, только что освободившегося из тюрьмы. Несмотря на то, что Филипп прикован к инвалидному креслу, Дриссу удается привнести в размеренную жизнь аристократа дух приключений.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Drama,
    //    Director = "Оливье Накаш",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/tTwFeGArcrs?si=3aZ1Puut2IAKOCID"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/535341/");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://okko.tv/movie/intouchables");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://rutube.ru/video/50d09cd2daa46a98172b8b7bb6846271/");
    //film2.Price.Add("Бесплатно");
    //film2.Advertisement.Add("С рекламой");
    //db.Film.AddRange(film1, film2);
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
