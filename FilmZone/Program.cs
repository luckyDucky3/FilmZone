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

    //Film film1 = new Film()
    //{
    //    Name = "Маленькие женщины",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b1/%D0%9C%D0%B0%D0%BB%D0%B5%D0%BD%D1%8C%D0%BA%D0%B8%D0%B5_%D0%B6%D0%B5%D0%BD%D1%89%D0%B8%D0%BD%D1%8B_%28%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%2C_2019%29.jpg",
    //    Description =
    //    "История взросления четырёх непохожих друг на друга сестер. Где-то бушует Гражданская война, но проблемы, с которыми сталкиваются девушки, актуальны как никогда: первая любовь, горькое разочарование, томительная разлука и непростые поиски себя и своего места в жизни.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Drama,
    //    Director = "Грета Гервиг",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/SnwKCS6kkvE?si=5RohyAKKrBnn0I_Z"
    //};
    //film1.Links.Add(@"https://kion.ru/video/movie/155747128");
    //film1.Price.Add("Платно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://bridzhit-dzhons.ru/malenkie-zhenschiny-2019/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("С рекламой");
    //Film film2 = new Film()
    //{
    //    Name = "Гордость и предубеждение",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/8/86/Pride_%26_Prejudice_2005.jpg",
    //    Description =
    //        "Англия, конец XVIII века. Родители пятерых сестер Беннет озабочены тем, чтобы удачно выдать дочерей замуж. И потому размеренная жизнь солидного семейства переворачивается вверх дном, когда по соседству появляется молодой джентльмен - мистер Бингли...",
    //    ReleaseFilmDate = 2005,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Джо Райт",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/kIwUj0X25ho?si=i3UPeAQ30ZKOuuPE"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/81733/");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://pride-prejudice.ru/1-2005/");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("Без рекламы");

    //Film film3 = new Film()
    //{
    //    Name = "Мальчик в полосатой пижаме",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1704946/84be7629-189d-4b13-8b35-fcf010512218/600x900",
    //    Description =
    //    "История, происходящая во время Второй мировой войны и показанная сквозь глаза невинного и ничего не подозревающего о происходящих событиях" +
    //    " Бруно, восьмилетнего сына коменданта концентрационного лагеря. Его случайное знакомство и дружба с еврейским мальчиком по другую сторону ограды лагеря, " +
    //    "в конечном счете, приводит к самым непредсказуемым и ошеломительным последствиям.",
    //    ReleaseFilmDate = 2008,
    //    Type = TypeFilm.Drama,
    //    Director = "Марк Херман",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/w_o1RQKbl6Q?si=tZHiskOYeGtfS6R4"
    //};
    //film3.Links.Add(@"https://rutube.ru/video/8a8ff15b51c0a2f8ac83809f30b2549e/");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("С рекламой");
    //film3.Links.Add(@"https://bogmedia.org/videos/1359/malchik-v-polosatoy-pijame/");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("С рекламой");

    //Film film4 = new Film()
    //{
    //    Name = "Список Шиндлера",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/3/38/Schindler%27s_List_movie.jpg",
    //    Description =
    //        "Фильм рассказывает реальную историю загадочного Оскара Шиндлера, члена нацистской партии, преуспевающего фабриканта, спасшего во время Второй мировой " +
    //        "войны почти 1200 евреев.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Biography,
    //    Director = "Стивен Спилберг",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/4r2Z0U9Y53o?si=uYt_ANH2RXHjpUdj"
    //};
    //film4.Links.Add(@"https://lordserials.cx/2983-schindlers-list.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("С рекламой");
    //film4.Links.Add(@"https://www.ivi.ru/watch/100131");
    //film4.Price.Add("Платно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Остров",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/b/b3/%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%D0%9E%D1%81%D1%82%D1%80%D0%BE%D0%B2.jpg/233px-%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%D0%9E%D1%81%D1%82%D1%80%D0%BE%D0%B2.jpg",
    //    Description =
    //    "Вторая мировая война. Баржу, на которой Анатолий и его старший товарищ Тихон перевозят уголь, захватывает немецкий сторожевой корабль. Вымаливая пощаду у немцев, Анатолий совершает предательство – расстреливает Тихона. Немцы оставляют труса на заминированной барже, но благодаря помощи монахов, проживающих в монастыре на острове, ему удается выжить.",
    //    ReleaseFilmDate = 2006,
    //    Type = TypeFilm.Drama,
    //    Director = "Павел Лунгин",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/mMe_VJTz33M?si=seoOC6MGSs48Z6lI"
    //};
    //film5.Links.Add(@"https://www.youtube.com/watch?v=qa_13X32DRg");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://rutube.ru/video/220d7ff7bc46da403af8ccff51e9a911/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //Film film6 = new Film()
    //{
    //    Name = "Бриджертоны",
    //    PathToImage = @"https://i.bigenc.ru/resizer/resize?sign=cLBJ1TTubCSxDUqvyf90zA&filename=vault/536d0926c893ab0c48dacceca10a06a8.webp&width=1200",
    //    Description =
    //        "История о восьми дружных братьях и сестрах влиятельной семьи Бриджертонов, которые пытаются найти любовь.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Том Верика, Триша Брок, Шери Фоксон",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/Cc6Z9TEHXV8?si=xpCSh_OwZ_vgIKMx"
    //};
    //film6.Links.Add(@"https://bridgerton.ru/");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("С рекламой");
    //film6.Links.Add(@"https://lordserials.fan/zarubezhnye-serialy/271-bridzhertony-v234.html");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("С рекламой");

    //Film film7 = new Film()
    //{
    //    Name = "Бумажный дом",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/8472ca52-2751-4bbe-9a08-8a1be75f93d5/220x330",
    //    Description =
    //    "История о преступниках, решивших ограбить Королевский монетный двор Испании и украсть 2,4 млрд евро.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.Detective,
    //    Director = "Хесус Кольменар, Алекс Родриго, Кольдо Серра",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/-ysaahmRuaA?si=S6X4RMLvcPDBgKYo"
    //};
    //film7.Links.Add(@"https://paper-house-tv.com/season-1/4-1-sezon-1-serija-soglasovannye-deistvija.html");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");
    //film7.Links.Add(@"https://lordserials.cx/1748-la-casa-de-papel.html");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");

    //Film film8 = new Film()
    //{
    //    Name = "Как избежать наказания за убийство",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1900788/bd6e89fb-0ee3-415f-9385-6c1a358dca57/600x900",
    //    Description =
    //        "Профессор Эннализ Китинг — блестящая адвокатесса. Она преподает студентам дисциплину под названием «Как избежать наказания за убийство»." +
    //        " Но те даже не подозревают, что в скором времени им придется применить свои знания в реальной жизни.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Detective,
    //    Director = "Билл Д’Элиа, Стивен Крегг, Лора Иннес",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/hAgggcrAjEU?si=SRmfYHb9EY-g4sPB"
    //};
    //film8.Links.Add(@"https://vk.com/video-105813732_456242902");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("С рекламой");

    //Film film1 = new Film()
    //{
    //    Name = "Территория",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/6/6f/%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%C2%AB%D0%A2%D0%B5%D1%80%D1%80%D0%B8%D1%82%D0%BE%D1%80%D0%B8%D1%8F%C2%BB.jpg",
    //    Description =
    //    "Территория - это место, где люди проверяются на прочность. Необозримые пространства, где тундра встречается с ледяными торосами Ледовитого океана. Суровый " +
    //    "русский север, которому способны бросить вызов немногие. Геолог Илья Чинков, одержимый идеей найти легендарное золото Территории, собирает команду смельчаков, " +
    //    "готовых поставить на карту все, включая собственные жизни.",
    //    ReleaseFilmDate = 2015,
    //    Type = TypeFilm.Drama,
    //    Director = "Александр Мельник",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/a4N57w1nbZc?si=2zcQxS-xaaEl_jaK"
    //};
    //film1.Links.Add(@"https://www.kinopoisk.ru/film/601933/");
    //film1.Price.Add("Платно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://www.ivi.ru/watch/126206");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("С рекламой");
    //Film film2 = new Film()
    //{
    //    Name = "Форрест Гамп",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/3560b757-9b95-45ec-af8c-623972370f9d/600x900",
    //    Description =
    //        "Сидя на автобусной остановке, Форрест Гамп — не очень умный, но добрый и открытый парень — рассказывает случайным встречным историю своей необыкновенной жизни.",
    //    ReleaseFilmDate = 1994,
    //    Type = TypeFilm.Comedy,
    //    Director = "Роберт Земекис",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/otmeAaifX04?si=O8u8pXgpMtT_Rcmb"
    //};
    //film2.Links.Add(@"https://kion.ru/video/movie/118278367");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://vk.com/video-223157201_456239063");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("Без рекламы");

    //Film film3 = new Film()
    //{
    //    Name = "Терминатор",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/c/ca/Terminator_poster.jpg",
    //    Description =
    //    "История противостояния солдата Кайла Риза и киборга-терминатора, прибывших в 1984 год из пост-апокалиптического будущего, где миром правят машины-убийцы, а человечество находится на грани вымирания.",
    //    ReleaseFilmDate = 1984,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "Джеймс Кэмерон",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/uA4k5Vc5jFc?si=4_7PhSWrIljmndj8"
    //};
    //film3.Links.Add(@"https://www.youtube.com/watch?v=iHD0JXpXKTQ");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("Без рекламы");
    //film3.Links.Add(@"https://rutube.ru/video/e64f1986c449d2ab5b58ecab25c16b16/");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("Без рекламы");

    //Film film4 = new Film()
    //{
    //    Name = "Звёздные войны. Эпизод 1: Скрытая угроза",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/4/40/Star_Wars_Phantom_Menace_poster.jpg",
    //    Description =
    //        "Мирная и процветающая планета Набу. Торговая федерация, не желая платить налоги, вступает в прямой конфликт с королевой Амидалой, правящей на планете, что приводит к войне. На стороне королевы и республики в ней участвуют два рыцаря-джедая: учитель и ученик, Квай-Гон-Джин и Оби-Ван Кеноби...",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Джордж Лукас",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/omuTFYiVqgA?si=-re_BoAfSWrk_QE-"
    //};
    //film4.Links.Add(@"https://rutube.ru/video/056125631a5a264ca7f40ec7fedb7519/");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"https://star-wars-films.ru/");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Человек дождя",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/5/54/Rain_man.jpg",
    //    Description =
    //    "Грубоватому и эгоистичному молодому человеку Чарли в наследство от отца достались лишь розовые кусты да «Бьюик» 1949 года, а львиная доля наследства уходит его брату-аутисту Раймонду. Задавшись целью отобрать «свою долю», Чарли похищает старшего брата. Но когда выясняет, что Раймонд обладает недюжими математическими способностями, памятью и внимательностью, решает использовать это в корыстных целях.",
    //    ReleaseFilmDate = 1988,
    //    Type = TypeFilm.Drama,
    //    Director = "Барри Левинсон",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/1AnTSk9YzKk?si=cDYspwq4xFyhEivV"
    //};
    //film5.Links.Add(@"https://rutube.ru/video/73bdc04748f4a38563e98677d448dbd0/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://www.hdfilmfinest.com/load/dramy/25110-chelovek-dozhdya-1988.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("С рекламой");
    //Film film6 = new Film()
    //{
    //    Name = "День сурка",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/4/41/Groundhog_Day.jpg",
    //    Description =
    //        "Телевизионный комментатор Фил Коннорс каждый год приезжает в маленький городок в штате Пенсильвания на празднование Дня сурка. Но на этот раз веселье рискует зайти слишком далеко. Время сыграло с ним злую шутку: оно взяло да и остановилось.",
    //    ReleaseFilmDate = 1993,
    //    Type = TypeFilm.Comedy,
    //    Director = "Харольд Рэмис",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/5Fsr_cJxag0?si=1FIHAutgsM7iPjz-"
    //};
    //film6.Links.Add(@"https://rutube.ru/video/7137fdcf6c797b32d455cc512a02a23b/");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("Без рекламы");
    //film6.Links.Add(@"https://www.hdfilmfinest.com/load/fehntezi/25682-den-surka.html");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("С рекламой");

    //Film film7 = new Film()
    //{
    //    Name = "Матрица",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4774061/cf1970bc-3f08-4e0e-a095-2fb57c3aa7c6/600x900",
    //    Description =
    //    "Жизнь Томаса Андерсона разделена на две части: днём он — самый обычный офисный работник, получающий нагоняи от начальства, а ночью превращается в хакера по имени Нео, и нет места в сети, куда он бы не смог проникнуть. Но однажды всё меняется. Томас узнаёт ужасающую правду о реальности.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "Лана Вачовски, Лилли Вачовски",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/YihPA42fdQ8?si=0v7IJuowJcQHfhfv"
    //};
    //film7.Links.Add(@"https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");
    //film7.Links.Add(@"https://matrix-films.ru/");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");

    //Film film8 = new Film()
    //{
    //    Name = "Назад в будущее",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/9/90/BTTF_DVD_rus.jpg",
    //    Description =
    //        "Подросток Марти с помощью машины времени, сооружённой его другом-профессором доком Брауном, попадает из 80-х в далекие 50-е. Там он встречается со своими " +
    //        "будущими родителями, ещё подростками, и другом-профессором, совсем молодым.",
    //    ReleaseFilmDate = 1985,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Роберт Земекис",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/ou8w0gQHlRE?si=jKRGFj_H0NutC33k"
    //};
    //film8.Links.Add(@"https://hd3.2-vse-chasti-filmov.xyz/34-nazad-v-budushee.html");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("С рекламой");
    //film8.Links.Add(@"https://www.ivi.ru/watch/90324");
    //film8.Price.Add("Платно");
    //film8.Advertisement.Add("Без рекламы");


    //Film film1 = new Film()
    //{
    //    Name = "Бегущий по лезвию",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/9/9b/%D0%91%D0%B5%D0%B3%D1%83%D1%89%D0%B8%D0%B9_%D0%BF%D0%BE_%D0%BB%D0%B5%D0%B7%D0%B2%D0%B8%D1%8E_2049.jpg",
    //    Description =
    //    "В недалеком будущем мир населен людьми и репликантами, созданными выполнять самую тяжелую работу. Работа офицера полиции Кей — держать репликантов под контролем в условиях нарастающего напряжения. Он случайно становится обладателем секретной информации, которая ставит под угрозу существование всего человечества. Желая найти ключ к разгадке, Кей решает разыскать Рика Декарда — бывшего офицера специального подразделения полиции Лос-Анджелеса, который бесследно исчез много лет назад.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "Дени Вильнёв",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/taQW31SVPCk?si=wtWT-4niissleLmj"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/fbd9299eca041a8260dcb83394271af0/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://kion.ru/video/movie/113571977");
    //film1.Price.Add("Платно");
    //film1.Advertisement.Add("Без рекламы");
    //Film film2 = new Film()
    //{
    //    Name = "Властелин колец: братство кольца",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/0/08/The_Lord_of_the_Rings._The_Fellowship_of_the_Ring_%E2%80%94_movie.jpg",
    //    Description =
    //        "Тихая деревня, где живут хоббиты. Придя на 111-й день рождения к своему старому другу Бильбо Бэггинсу, волшебник Гэндальф начинает вести разговор о кольце, которое Бильбо нашел много лет назад. Это кольцо принадлежало когда-то темному властителю Средиземья Саурону, и оно дает большую власть своему обладателю. Теперь Саурон хочет вернуть себе власть над Средиземьем. Бильбо отдает Кольцо племяннику Фродо, чтобы тот отнёс его к Роковой Горе и уничтожил.",
    //    ReleaseFilmDate = 2001,
    //    Type = TypeFilm.Adventures,
    //    Director = "Питер Джексон",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/RNksw9VU2BQ?si=0hF2kbbNe_IDMuzG"
    //};
    //film2.Links.Add(@"https://rutube.ru/video/6a20ed30f8b1368f21d36605b43f8ba0/");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://lord-rings-lordfilm.cam/vlastelin-kolets-bratstvo-koltsa");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("С рекламой");

    //Film film3 = new Film()
    //{
    //    Name = "Молчание ягнят",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/9c655ce1-2b59-4941-97aa-f9e928ed0ff4/600x900",
    //    Description =
    //    "Психопат похищает и убивает молодых женщин по всему Среднему Западу. ФБР, уверенное, что все преступления совершены одним и тем же человеком, поручает агенту Клариссе Старлинг встретиться с заключенным-маньяком Ганнибалом Лектером, который мог бы помочь составить психологический портрет убийцы. Сам Лектер отбывает наказание за убийства и каннибализм. Он согласен помочь Клариссе лишь в том случае, если она попотчует его больное воображение подробностями своей личной жизни.",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Horrors,
    //    Director = "Джонатан Демме",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/9JTNhAOVosk?si=09KUt7Fge6fKAf1a"
    //};
    //film3.Links.Add(@"https://rutube.ru/video/a4e9818b98f7057404c538d1807e7629/");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("Без рекламы");
    //film3.Links.Add(@"https://lordserials.cx/2990-the-silence-of-the-lambs.html");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("Без рекламы");

    //Film film4 = new Film()
    //{
    //    Name = "Бегущий в лабиринте",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/d7ad29b7-da97-42b4-9669-c14a1a822591/600x900",
    //    Description =
    //        "Главный герой — подросток Томас, который просыпается в лифте, но ничего не помнит, кроме своего имени. Он оказывается среди других подростков, научившихся выживать в замкнутом пространстве. Раз в 30 дней прибывает новый мальчик. Группа ребят проживает в «Приюте» уже три года. Они кормятся тем, что удается вырастить на земле, и пытаются найти выход из лабиринта, окружающего их место жительства. Но однажды появляется девочка в состоянии комы...",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Уэс Болл",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/lgqluXtxL9Y?si=G-SIk9B7Aafb49ed"
    //};
    //film4.Links.Add(@"https://rutube.ru/video/8da63782936497a7bc06d5699df4541e/");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"https://beguschii-v-labirinte.ru/");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Побег из Шоушенка",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/d/de/Movie_poster_the_shawshank_redemption.jpg",
    //    Description =
    //    "Бухгалтер Энди Дюфрейн обвинён в убийстве собственной жены и её любовника. Оказавшись в тюрьме под названием Шоушенк, он сталкивается с жестокостью и беззаконием, царящими по обе стороны решётки. Каждый, кто попадает в эти стены, становится их рабом до конца жизни. Но Энди, обладающий живым умом и доброй душой, находит подход как к заключённым, так и к охранникам, добиваясь их особого к себе расположения.",
    //    ReleaseFilmDate = 1994,
    //    Type = TypeFilm.Drama,
    //    Director = "Фрэнк Дарабонт",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/kgAeKpAPOYk?si=tyvH-TJ4HS-KYAa1"
    //};
    //film5.Links.Add(@"https://rutube.ru/video/cae96c24f81419ccd444ed151a169985/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://lordserials.cx/2970-the-shawshank-redemption.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("С рекламой");
    //Film film6 = new Film()
    //{
    //    Name = "Энола Холмс",
    //    PathToImage = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP8kErB6Gym961lBx1Trsp5nRSWbM68IWPrjqU4cXN5w&s",
    //    Description =
    //        "Англия, 1884 год. В день своего 16-летия Энола Холмс обнаруживает, что её мама бесследно пропала. Обратившись за помощью к давно покинувшим отчий дом братьям Майкрофту и Шерлоку, девушка не получает ожидаемой поддержки. Поскольку отец умер рано, мать — женщина прогрессивных взглядов — воспитывала и обучала дочь сама, и теперь юная Энола не соответствует общепринятому представлению о приличной молодой особе. Хоть Шерлок и сочувствует сестре, Майкрофт является её законным опекуном и собирается отправить в женский пансион. Будучи не в восторге от данной перспективы, Энола начинает разгадывать оставленные матерью подсказки и, переодевшись мальчиком, тайком садится на поезд в Лондон, где знакомится с молодым виконтом, тоже сбежавшим от своей родни.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Adventures,
    //    Director = "Гарри Брэдбир",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/AVU3SI3Jb5Y?si=dohG6hxcTwanlKk6"
    //};
    //film6.Links.Add(@"https://rutube.ru/video/d4b97faede9fa60d38ad259ee64498cd/");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("Без рекламы");
    //film6.Links.Add(@"https://zonafilm.ru/movies/enola-holms");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("Без рекламы");

    //Film film7 = new Film()
    //{
    //    Name = "Зеленая миля",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b0/Green_mile_film.jpg",
    //    Description =
    //    "Пол Эджкомб — начальник блока смертников в тюрьме «Холодная гора», каждый из узников которого однажды проходит «зеленую милю» по пути к месту казни. Пол повидал много заключённых и надзирателей за время работы. Однако гигант Джон Коффи, обвинённый в страшном преступлении, стал одним из самых необычных обитателей блока.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Фрэнк Дарабонт",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/TODt_q-_4C4?si=LLUVL30_D-_DEUXe"
    //};
    //film7.Links.Add(@"https://vk.com/video-220018529_456240901");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");
    //film7.Links.Add(@"https://www.hdfilmfinest.com/load/fehntezi/26066-zelenaya-milya.html");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");

    //Film film8 = new Film()
    //{
    //    Name = "Форсаж",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/3/3b/The_Fast_and_the_Furious.jpg/212px-The_Fast_and_the_Furious.jpg",
    //    Description =
    //        "Его зовут Брайан, и он — фанат турбин и нитроускорителей. Он пытается попасть в автобанду легендарного Доминика Торетто, чемпиона опасных и незаконных уличных гонок. Брайан также полицейский, и его задание — втереться в доверие к Торетто, подозреваемому в причастности к дерзким грабежам грузовиков, совершаемым прямо на ходу.",
    //    ReleaseFilmDate = 2001,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "Роб Коэн",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/8QyEruRwNeo?si=QI5AKI6RJ7BTI9cu"
    //};
    //film8.Links.Add(@"https://www.kinopoisk.ru/film/666/");
    //film8.Price.Add("Платно");
    //film8.Advertisement.Add("Без рекламы");
    //film8.Links.Add(@"https://tv.forsazh-lordfilm.com/115-forsazh.html");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("С рекламой");

    //Film film1 = new Film()
    //{
    //    Name = "Тоня против всех",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/d/d3/I%2C_Tonya.jpg",
    //    Description =
    //    "Американской фигуристке Тоне Хардинг пришлось нелегко: сначала тяжелое детство с грозной матерью, потом тяжелая юность, ранний брак с проходимцем и неудачи на соревнованиях из-за заниженных судьями оценок. А потом случился скандал: во время важнейших соревнований план запугать конкурентку выходит Тоне боком.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.Drama,
    //    Director = "Крэйг Гиллеспи",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/y1Nx9r1rQQQ?si=y1GbN5dagbiDgs9O"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/b0ba205adefeddcb3fe536c20fe417c5/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://www.kinopoisk.ru/film/1008385/");
    //film1.Price.Add("Платно");
    //film1.Advertisement.Add("Без рекламы");
    //Film film2 = new Film()
    //{
    //    Name = "Ла-ла-ленд",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/e/e4/La_La_Land.jpg",
    //    Description =
    //        "Это история любви старлетки, которая между прослушиваниями подает кофе состоявшимся кинозвездам, и фанатичного джазового музыканта, вынужденного подрабатывать в заштатных барах. Но пришедший к влюбленным успех начинает подтачивать их отношения.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Musical,
    //    Director = "Дэмьен Шазелл",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/lneNCBIXD4I?si=bAoodZAl_8_Fy8um"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/841081/");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://www.ivi.ru/watch/136478");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("С рекламой");

    //Film film3 = new Film()
    //{
    //    Name = "Лето. Одноклассники. Любовь",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/38839aae-c172-4ead-a082-2847d086cfb3/600x900",
    //    Description =
    //    "Лола, или Лол, как ее зовут друзья, живет в пригороде Чикаго, как и все, прогуливает занятия, чатится с подругами, устраивает вечеринки и мечтает о настоящей любви. Поездка в Париж – это шанс для Лолы и ее одноклассников оторваться по полной. Безудержный шопинг, бесконтрольные ночные вылазки и первое романтическое свидание – все это оказывается под угрозой. Ведь мать Лолы, мягко говоря, не обрадована результатами ее экзаменов.",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Лиза Азуэлос",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/5qdS0lfqR7k?si=ukcHBAXU-a1_dMZL"
    //};
    //film3.Links.Add(@"https://new.filmhd1080.sbs/3336-1104-film-leto-odnoklassniki-lyubov-2011-smotret-onlayn.html");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("С рекламой");
    //film3.Links.Add(@"https://www.hdfilmfinest.com/load/melodramy/14988-leto-odnoklassniki-lyubov.html");
    //film3.Price.Add("Бесплатно");
    //film3.Advertisement.Add("С рекламой");

    //Film film4 = new Film()
    //{
    //    Name = "Игра престолов",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/4/49/Game_of_Thrones.jpg/262px-Game_of_Thrones.jpg",
    //    Description =
    //        "К концу подходит время благоденствия, и лето, длившееся почти десятилетие, угасает. Вокруг средоточия власти Семи королевств, Железного трона, зреет заговор, и в это непростое время король решает искать поддержки у друга юности Эддарда Старка. В мире, где все — от короля до наемника — рвутся к власти, плетут интриги и готовы вонзить нож в спину, есть место и благородству, состраданию и любви. Между тем никто не замечает пробуждение тьмы из легенд далеко на Севере — и лишь Стена защищает живых к югу от нее.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Дэвид Наттер, Алан Тейлор, Алекс Грейвз",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/edBuDu7QE14?si=czcDkJUTKZ0FOdYw"
    //};
    //film4.Links.Add(@"https://www.kinopoisk.ru/series/464963/");
    //film4.Price.Add("Платно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"http://thrones-online.com/season-1/4-1-sezon-1-seriya-zima-blizko-1.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("С рекламой");


    //Film film5 = new Film()
    //{
    //    Name = "Офис",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/7bbd225f-e6db-4326-b600-1ac294cf9d99/600x900",
    //    Description =
    //    "Сериал о трудовых буднях небольшого регионального офиса крупной компании, обитатели которого целыми днями должны терпеть закидоны своего непутевого босса.",
    //    ReleaseFilmDate = 2005,
    //    Type = TypeFilm.Comedy,
    //    Director = "Пол Фиг, Рэндолл Айнхорн, Кен Куопис",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/6aCZSPJwvbk?si=OwO_q1jZiuLaoXRm"
    //};
    //film5.Links.Add(@"https://officeserial.ru/seasons/season-1/episode-1");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://rutube.ru/video/bd5381d57eea24963dd12f57cdd28dbb/?playlist=338124&playlistPage=1");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //Film film6 = new Film()
    //{
    //    Name = "Твин Пикс",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/5/55/Twinpeaks2.jpg",
    //    Description =
    //        "История начинается с известия о находке обнаженного тела старшеклассницы Лоры Палмер, завёрнутого в полиэтилен и выброшенного волнами на берег озера. В ходе расследования перед внимательными взглядами агента Купера, шерифа Трумана и его помощников проходят разные жители Твин Пикс. Постепенно зритель открывает для себя темную и страшную сторону жизни обитателей на первый взгляд тихого и мирного городка.",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Drama,
    //    Director = "Дэвид Линч, Лесли Линка Глаттер, Калеб Дешанель",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/4-MYnxhcElk?si=DZ17gqHGHCq_R780"
    //};
    //film6.Links.Add(@"https://www.kinopoisk.ru/series/84358/");
    //film6.Price.Add("Платно");
    //film6.Advertisement.Add("Без рекламы");
    //film6.Links.Add(@"https://lordserials.cx/491-twin-peaks.html");
    //film6.Price.Add("Беслатно");
    //film6.Advertisement.Add("С рекламой");

    //Film film7 = new Film()
    //{
    //    Name = "Остаться в живых",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/e44eb2d3-9a69-4d03-b851-88f694c4fd55/600x900",
    //    Description =
    //    "Красавец-лайнер, совершающий полет из Сиднея в Лос-Анджелес, неожиданно терпит крушение. 48 пассажиров оказываются на пустынном острове посреди океана. Люди в панике.",
    //    ReleaseFilmDate = 2004,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Джек Бендер, Стивен Уильямс, Пол А. Эдвардс",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/nyXAPpf9Z58?si=TBH-BsB2EtDAT4O_"
    //};
    //film7.Links.Add(@"https://rutube.ru/channel/35825560/");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");
    //film7.Links.Add(@"https://lost-tv.online/episodes/1-sezon-1-seriya/");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");

    //Film film8 = new Film()
    //{
    //    Name = "Черное зеркало",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/a3ae9e25-5b10-42f3-96ae-5d707fc6a1bc/600x900",
    //    Description =
    //        "За последние годы технологии всесторонне изменили нашу жизнь, прежде чем мы успели опомниться и усомниться в них. В каждом доме, на каждом столе, на каждой ладони — плазменный телевизор, монитор компьютера, дисплей смартфона — чёрное зеркало нашего существования в XXI веке.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Drama,
    //    Director = "Оуэн Харрис, Карл Тиббеттс, Джеймс Хоуз",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/11Dlkoi2u6M?si=W-JSSDCMjAfAfQ7c"
    //};
    //film8.Links.Add(@"https://rutube.ru/channel/31479213/");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("Без рекламы");
    //film8.Links.Add(@"https://chernoe-zerkalo.ru/1-season/4-e1.html");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("С рекламой");


    Film film1 = new Film()
    {
        Name = "Аббатство Даунтон",
        PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/8/8c/DowntonAbbey2019Poster.jpg",
        Description =
        "1912 год. Англия. Наследник титула графа Грэнтэма, живущего с семьей в своем родовом имении Даунтон, погибает на «Титанике». Семья ожидает, что теперь, когда наследников мужского пола не осталось, владения и капитал семьи после смерти графа перейдут к его старшей дочери. Но граф, отдавший всю свою жизнь своему поместью, отказывается отстаивать права юной Мэри, считая, что все, включая немалый капитал его жены, должно отойти к наследнику его графского титула, безвестному дальнему родственнику...",
        ReleaseFilmDate = 2010,
        Type = TypeFilm.Drama,
        Director = "Брайан Персивал, Дэвид Эванс, Филип Джон",
        FilmOrSerial = FilmOrSerial.Serial,
        Preview = @"https://www.youtube.com/embed/zpuOHrgUUbA?si=xELjBkmxBXGFeE69"
    };
    film1.Links.Add(@"https://www.youtube.com/playlist?list=PL73orRnLash5Xnunc27R6jOna_5Wj-aZM");
    film1.Price.Add("Бесплатно");
    film1.Advertisement.Add("Без рекламы");
    film1.Links.Add(@"https://downton-baibakotv.net/");
    film1.Price.Add("Бесплатно");
    film1.Advertisement.Add("C рекламой");
    Film film2 = new Film()
    {
        Name = "Анатомия страсти",
        PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/2/2c/Grey%27s_Anatomy_s3.jpg/274px-Grey%27s_Anatomy_s3.jpg",
        Description =
            "В центре событий — молодая женщина-хирург Мередит Грей, дочь известного врача Эллис Грей. Она, как и её коллеги — такие же начинающие врачи, как она — работает в городской больнице Сиэтла. Хирурги оперируют и влюбляются, заводят истории болезни и служебные романы, хранят свои врачебные тайны, борются с осложнениями у пациентов и в собственной личной жизни. И зачастую отношения с противоположным полом волнуют их не меньше, чем вопрос приобретения ими профессионального опыта.",
        ReleaseFilmDate = 2015,
        Type = TypeFilm.Drama,
        Director = "Роб Корн, Кевин МакКидд, Дебби Аллен",
        FilmOrSerial = FilmOrSerial.Serial,
        Preview = @"https://www.youtube.com/embed/6NGKYeq4nZc?si=dDth6h96rdDfJvpt"
    };
    film2.Links.Add(@"https://serial-time.net/62-8-anatomiya-strasti-1-sezon-s14.html");
    film2.Price.Add("Беслатно");
    film2.Advertisement.Add("Без рекламы");
    film2.Links.Add(@"https://greyanatomy.ru/seasons/season-1/episode-1");
    film2.Price.Add("Беслатно");
    film2.Advertisement.Add("Без рекламы");

    
    db.Film.AddRange(film1, film2);
    db.SaveChanges();

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
