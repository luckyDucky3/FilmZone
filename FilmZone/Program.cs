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
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<TimerHostedService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<TimerHostedService>());


//Console.WriteLine("===============conclusion=================");
//using (ApplicationDbContext db = new ApplicationDbContext()) //добавление данных
//{
//    try
//    {
//        db.SaveChanges();
//        Console.WriteLine("Access to the database has been successfully obtained");
//    }
//    catch (DbUpdateConcurrencyException ex)
//    {
//        Console.WriteLine($"DbUpdateConcurrencyException, {ex}");
//    }
//    catch (DbUpdateException ex)
//    {
//        Console.WriteLine($"DbUpdateException, {ex}");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Database access failed, {ex}");
//    }
    
    //if()
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


    //Film film1 = new Film()
    //{
    //    Name = "Один дома",
    //    PathToImage = @"https://klike.net/uploads/posts/2020-04/1586848216_4.jpg",
    //    Description =
    //    "Американское семейство отправляется из Чикаго в Европу, но в спешке сборов бестолковые родители забывают дома... одного из своих детей. Юное создание, однако, не теряется и демонстрирует чудеса изобретательности. И когда в дом залезают грабители, им приходится не раз пожалеть о встрече с милым крошкой. ",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Comedy,
    //    Director = "Крис Коламбус",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/bBU_64CTNsw?si=ex8YAvKaWHFcNJ8s"
    //};
    //film1.Links.Add(@"https://my.mail.ru/mail/nata.ermolenko.1968/video/_myvideo/218.html");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://hd.odindomalordfilm.ru/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //Film film2 = new Film()
    //{
    //    Name = "Холоп 2",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4483445/42253d2f-65fc-4f0b-b95d-a8fa9b169502/1920x",
    //    Description =
    //        "Гриша, бывший мажор, побывавший холопом и ставший человеком, после путешествия в «прошлое» чутко реагирует на любую несправедливость. И, конечно, не может пройти мимо беспредела, который творит наглая и избалованная Катя. Ничего удивительного, что вскоре мажорка обнаруживает себя в другом времени.",
    //    ReleaseFilmDate = 2023,
    //    Type = TypeFilm.Comedy,
    //    Director = "Клим Шипенко",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/jmq-JT7Az1w?si=UP0ImWHw4UzCK9KJ"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/5047468/");
    //film2.Price.Add("Платно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://ru-film-v4.lordfilm.limo/222-test-x-m7.html");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("С рекламой");
    //Film film3 = new Film()
    //{
    //    Name = "Холоп",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/ea280ff7-1989-44e4-97a8-e2598aa951a4/600x900",
    //    Description =
    //    "27-летний московский мажор Григорий ошалел от безнаказанности. Богатый папа стабильно его отмазывает, да так, что уже обновил автопарк и оборудование отделению полиции, где служит начальником его друг. После очередной выходки терпение отца иссякает, и он обращается к психологу, практикующему шоковые методы воздействия на пациентов. \r\nВскоре сынуля попадает в аварию и приходит в себя на деревенской конюшне. На дворе — Россия 1860 года, а сам он — бесправный конюх Гришка, которому за любую провинность и ослушание всегда готовы всыпать плетей, а то и вздёрнуть на глазах у всего честного народа.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Comedy,
    //    Director = "Клим Шипенко",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/C8m6K_Er3BI?si=lJ-5NPGud_-he5PT"
    //};
    //film3.Links.Add(@"https://hd.kinopoisk.ru/film/4cc6fae94f652033b3d127ecbe373205");
    //film3.Price.Add("Платно");
    //film3.Advertisement.Add("Без рекламы");
    //film3.Links.Add(@"https://premier.one/show/holop");
    //film3.Price.Add("Платно");
    //film3.Advertisement.Add("Без рекламы");
    //Film film4 = new Film()
    //{
    //    Name = "Бриллиантовая рука",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/10893610/32ef92db-5cbb-426d-acf9-2327b695edf8/600x900",
    //    Description =
    //        "В южном городке орудует шайка валютчиков, возглавляемая Шефом и его помощником Графом (в быту — Геной Козодоевым). Скромный советский служащий и примерный семьянин Семен Семенович Горбунков отправляется в зарубежный круиз на теплоходе, где также плывет Граф, который должен забрать бриллианты в одном из восточных городов и провезти их в загипсованной руке. Но из-за недоразумения вместо жулика на условленном месте падает ничего не подозревающий Семен Семенович, и драгоценный гипс накладывают ему.",
    //    ReleaseFilmDate = 1968,
    //    Type = TypeFilm.Comedy,
    //    Director = "Леонид Гайдай",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/aDvuA9xGVbE?si=NQFLkCQjj1QV_QP7"
    //};
    //film4.Links.Add(@"https://www.kinopoisk.ru/film/46225/");
    //film4.Price.Add("Платно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"https://my.mail.ru/v/ussr_hd/video/films/1768.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Круэлла",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/9bb0f260-1c5f-4698-91cc-de481bfd0f55/600x900",
    //    Description =
    //    "Великобритания, 1960-е годы. Эстелла была необычным ребёнком, и особенно трудно ей было мириться со всякого рода несправедливостью. Вылетев из очередной школы, она с мамой отправляется в Лондон. По дороге они заезжают в особняк известной модельерши по имени Баронесса, где в результате ужасного несчастного случая мама погибает. Добравшись до Лондона, Эстелла знакомится с двумя мальчишками — уличными мошенниками Джаспером и Хорасом.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Comedy,
    //    Director = "Крэйг Гиллеспи",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/9F2-eR2dfMY?si=2RTrwUknUX9W3jfu"
    //};
    //film5.Links.Add(@"https://wax.www-lord.stream/317-film-krujella.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("C рекламой");
    //film5.Links.Add(@"https://my.mail.ru/mail/alive-bars/video/_myvideo/412.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //Film film6 = new Film()
    //{
    //    Name = "Лулу и Бриггс",
    //    PathToImage = @"https://kinoafisha.ua/upload/2021/12/films/9751/23hj4w7kdog.jpeg",
    //    Description =
    //        "Профессиональный военный Джексон Бриггс всеми силами пытается вернуться в строй, но из-за травмы головы получает постоянные отказы. Когда умирает один из его сослуживцев, Бриггсу дают задание: с военной базы в штате Вашингтон доставить на похороны в аризонский Ногалес боевую подругу почившего — нервную бельгийскую овчарку Лулу с целым спектром посттравматических расстройств. Поскольку собака боится летать, сделать это придётся на машине по Тихоокеанскому побережью, и эта поездка будет не самым простым заданием Бриггса.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Drama,
    //    Director = "Рид Кэролин, Ченнинг Татум",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/W2uQ1wJOPRk?si=-A6aZ7vgI6hyNaxd"
    //};
    //film6.Links.Add(@"https://d.lordfilm15.site/65-lulu-i-briggs.html");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("C рекламой");
    //film6.Links.Add(@"https://www.kinopoisk.ru/film/1355139/");
    //film6.Price.Add("Платно");
    //film6.Advertisement.Add("Без рекламы");
    //Film film7 = new Film()
    //{
    //    Name = "Паразиты",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/aae3a928-6465-4bed-9af4-16929a44fd79/600x900",
    //    Description =
    //    "Обычное корейское семейство Кимов жизнь не балует. Приходится жить в сыром грязном полуподвале, воровать интернет у соседей и перебиваться случайными подработками. Однажды друг сына семейства, уезжая на стажировку за границу, предлагает тому заменить его и поработать репетитором у старшеклассницы в богатой семье Пак. Подделав диплом о высшем образовании, парень отправляется в шикарный дизайнерский особняк и производит на хозяйку дома хорошее впечатление. Тут же ему в голову приходит необычный план по трудоустройству сестры.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Drama,
    //    Director = "Пон Джун-хо",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/GGnM74uxjlo?si=XpeqjTg1W_I0O6CG"
    //};
    //film7.Links.Add(@"https://my.mail.ru/mail/kremlev.vladimir/video/453/91183.html");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");
    //film7.Links.Add(@"https://www.kinopoisk.ru/film/1043758/");
    //film7.Price.Add("Платно");
    //film7.Advertisement.Add("Без рекламы");
    //Film film8 = new Film()
    //{
    //    Name = "Поезд в Пусан",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/12feddd0-1f10-4608-b6c7-2d0bad2bdceb/600x900",
    //    Description =
    //        "У маленькой Су-ан день рождения. Девочка живет с отцом в Сеуле и очень хочет отправиться к маме в Пусан. По дороге случается непредвиденное, и на страну обрушивается загадочный вирус. Пассажирам поезда в Пусан — единственного города, отразившего атаки вируса — придется бороться за выживание. 442 километра в пути. Добро пожаловать на борт и помните — в этой гонке недостаточно выжить, чтобы остаться человеком",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Horrors,
    //    Director = "Ён Сан-хо",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/58r-Rq_TuEI?si=qhx7pFpt4ImI93Cw"
    //};
    //film8.Links.Add(@"https://my.mail.ru/mail/raskovalov64/video/_myvideo/15950.html");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("Без рекламы");
    //film8.Links.Add(@"https://www.kinopoisk.ru/film/977288/");
    //film8.Price.Add("Платно");
    //film8.Advertisement.Add("Без рекламы");

    //Film film1 = new Film()
    //{
    //    Name = "Служанка",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/757f5bd9-b85c-4220-9b8d-5548a3436937/600x900",
    //    Description =
    //    "1930-е годы, Корея под властью Японии. Мошенник по прозвищу Граф предлагает бедной девушке Сук-хи поучаствовать в деле, обещая солидный заработок. Он рассчитывает очаровать богатую японку Хидэко, сбежать с ней в Японию, жениться, затем объявить её сумасшедшей, упечь в дурдом и завладеть богатством. А Сук-хи должна ему помочь, нанявшись в услужение к Хидэко. Что она и сделала. Но внезапно идеальный план Графа оказывается под угрозой.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Thriller,
    //    Director = "Пак Чхан-ук",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/-nF9DLj8YcQ?si=J2edVtkQpwf3W0lQ"
    //};
    //film1.Links.Add(@"https://my.mail.ru/mail/nursik_bekkozha/video/_myvideo/11.html");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //film1.Links.Add(@"https://b.lordfilms0.org/425-sluzhanka-film-2016-06");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("С рекламой");
    //Film film2 = new Film()
    //{
    //    Name = "Олдбой",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/f91ef1b3-aeff-4835-8117-8a23781f2533/600x900 ",
    //    Description =
    //        "1988 год. Обыкновенный и ничем непримечательный бизнесмен О Дэ-cу в день трёхлетия дочери по пути домой напивается, начинает хулиганить и закономерно попадает в полицейский участок. Из участка его под своё поручительство забирает друг детства. Пока тот звонит жене незадачливого пьяницы, О Дэ-су пропадает. Неизвестные похищают его и помещают в комнату без окон на 15 лет.",
    //    ReleaseFilmDate = 2003,
    //    Type = TypeFilm.Thriller,
    //    Director = "Пак Чхан-ук",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/vtaNksc6roc?si=f7_--UxGva5uvsGF"
    //};
    //film2.Links.Add(@"https://my.mail.ru/mail/grandcoosh/video/26/1632.html");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://hd1.33lordfilm-0.xyz/33944-oldboi.html");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("С рекламой");
    //Film film3 = new Film()
    //{
    //    Name = "Волк с Уолл-стрит",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1946459/5c758ac0-7a5c-4f00-a94f-1be680a312fb/600x900",
    //    Description =
    //    "1987 год. Джордан Белфорт становится брокером в успешном инвестиционном банке. Вскоре банк закрывается после внезапного обвала индекса Доу-Джонса. По совету жены Терезы Джордан устраивается в небольшое заведение, занимающееся мелкими акциями. Его настойчивый стиль общения с клиентами и врождённая харизма быстро даёт свои плоды. Он знакомится с соседом по дому Донни, торговцем, который сразу находит общий язык с Джорданом и решает открыть с ним собственную фирму. В качестве сотрудников они нанимают нескольких друзей Белфорта, его отца Макса и называют компанию «Стрэттон Оукмонт». В свободное от работы время Джордан прожигает жизнь: лавирует от одной вечеринки к другой, вступает в сексуальные отношения с проститутками, употребляет множество наркотических препаратов, в том числе кокаин и кваалюд. Однажды наступает момент, когда быстрым обогащением Белфорта начинает интересоваться агент ФБР...",
    //    ReleaseFilmDate = 2013,
    //    Type = TypeFilm.Drama,
    //    Director = "Мартин Скорсезе",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/CHivqmutR0I?si=H5ZIvm5RvUqH1LLe"
    //};
    //film3.Links.Add(@"https://www.kinopoisk.ru/film/462682/");
    //film3.Price.Add("Платно");
    //film3.Advertisement.Add("Без рекламы");
    //Film film4 = new Film()
    //{
    //    Name = "Достать ножи",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/bb8afbd6-c9cd-4631-99e9-3fecf241dbaf/600x900",
    //    Description =
    //        "На следующее утро после празднования 85-летия известного автора криминальных романов Харлана Тромби виновника торжества находят мёртвым. Налицо — явное самоубийство, но полиция по протоколу опрашивает всех присутствующих в особняке членов семьи, хотя, в этом деле больше заинтересован частный детектив Бенуа Блан. Тем же утром он получил конверт с наличными от неизвестного и заказ на расследование смерти Харлана. Не нужно быть опытным следователем, чтобы понять, что все приукрашивают свои отношения с почившим главой семейства, но Блану достаётся настоящий подарок — медсестра покойного, которая физически не выносит ложь.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Detective,
    //    Director = "Райан Джонсон",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/8VMvCavnFNw?si=B3o0aGUwAa6u7Wx3"
    //};
    //film4.Links.Add(@"https://my.mail.ru/mail/vladimir.1336/video/211/22145.html");
    //film4.Price.Add("Платно");
    //film4.Advertisement.Add("Без рекламы");
    //film4.Links.Add(@"https://www.kinopoisk.ru/film/1188529/");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Зеленая книга",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/4b27e219-a8a5-4d85-9874-57d6016e0837/600x900",
    //    Description =
    //    "1960-е годы. После закрытия нью-йоркского ночного клуба на ремонт вышибала Тони по прозвищу Болтун ищет подработку на пару месяцев. Как раз в это время Дон Ширли — утонченный светский лев, богатый и талантливый чернокожий музыкант, исполняющий классическую музыку — собирается в турне по южным штатам, где ещё сильны расистские убеждения и царит сегрегация. Он нанимает Тони в качестве водителя, телохранителя и человека, способного решать текущие проблемы. У этих двоих так мало общего, и эта поездка навсегда изменит жизнь обоих.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Biography,
    //    Director = "Питер Фаррелли",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/e6b9urtUJt0?si=YJbU1fLuxHjIdomT"
    //};
    //film5.Links.Add(@"https://my.mail.ru/mail/avan.mad/video/_myvideo/66.html");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://www.kinopoisk.ru/film/1108577/");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //Film film6 = new Film()
    //{
    //    Name = "Бойцовский клуб",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/8ef070c9-2570-4540-9b83-d7ce759c0781/600x900",
    //    Description =
    //        "Сотрудник страховой компании страдает хронической бессонницей и отчаянно пытается вырваться из мучительно скучной жизни. Однажды в очередной командировке он встречает некоего Тайлера Дёрдена — харизматического торговца мылом с извращенной философией. Тайлер уверен, что самосовершенствование — удел слабых, а единственное, ради чего стоит жить, — саморазрушение. \r\nПроходит немного времени, и вот уже новые друзья лупят друг друга почем зря на стоянке перед баром, и очищающий мордобой доставляет им высшее блаженство. Приобщая других мужчин к простым радостям физической жестокости, они основывают тайный Бойцовский клуб, который начинает пользоваться невероятной популярностью.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Thriller,
    //    Director = "Дэвид Финчер",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/C7-7qQ61QHU?si=eiIEFHCSTLTL65uq"
    //};
    //film6.Links.Add(@"https://my.mail.ru/mail/vm_gluschenko/video/110735/237319.html");
    //film6.Price.Add("Бесплатно");
    //film6.Advertisement.Add("Без рекламы");
    //film6.Links.Add(@"https://www.kinopoisk.ru/film/361/");
    //film6.Price.Add("Платно");
    //film6.Advertisement.Add("Без рекламы");
    //Film film7 = new Film()
    //{
    //    Name = "Зверополис",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4716873/893bba73-3105-4944-8d18-a38f929f2759/600x900",
    //    Description =
    //    "Добро пожаловать в Зверополис – современный город, населенный самыми разными животными, от огромных слонов до крошечных мышек. Зверополис разделен на районы, полностью повторяющие естественную среду обитания разных жителей – здесь есть и элитный район Площадь Сахары и неприветливый Тундратаун. В этом городе появляется новый офицер полиции, жизнерадостная зайчиха Джуди Хоппс, которая с первых дней работы понимает, как сложно быть маленькой и пушистой среди больших и сильных полицейских. Джуди хватается за первую же возможность проявить себя, несмотря на то, что ее партнером будет болтливый хитрый лис Ник Уайлд. Вдвоем им предстоит раскрыть сложное дело, от которого будет зависеть судьба всех обитателей Зверополиса.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Cartoon,
    //    Director = "Байрон Ховард, Рич Мур, Джаред Буш",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/N6zm52tRF0c?si=WsXBDfCCi7IdbqDs"
    //};
    //film7.Links.Add(@"https://wax.www-lord.stream/308-multfilm-zveropolis.html ");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");
    //film7.Links.Add(@"https://vk.com/video-188008198_456239133");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("Без рекламы");
    //Film film8 = new Film()
    //{
    //    Name = "Остров проклятых",
    //    PathToImage = @"https://b1.filmpro.ru/c/102900.jpg",
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



    //Film film1 = new Film()
    //{
    //    Name = "Начало",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1629390/8ab9a119-dd74-44f0-baec-0629797483d7/600x900",
    //    Description =
    //    "Кобб – талантливый вор, лучший из лучших в опасном искусстве извлечения: он крадет ценные секреты из глубин подсознания во время сна, когда человеческий разум наиболее уязвим. Редкие способности Кобба сделали его ценным игроком в привычном к предательству мире промышленного шпионажа, но они же превратили его в извечного беглеца и лишили всего, что он когда-либо любил.",
    //    ReleaseFilmDate = 2010,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Кристофер Нолан",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/85Zz1CCXyDI?si=eHJHC_sqiClycuSV"
    //};
    //film1.Links.Add(@"https://nachalo-lordfilm.ru/");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("С рекламой");
    //film1.Links.Add(@"https://my.mail.ru/list/svarog.1975/video/670/69216.html ");
    //film1.Price.Add("Бесплатно");
    //film1.Advertisement.Add("Без рекламы");
    //Film film2 = new Film()
    //{
    //    Name = "Титаник",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/96d93e3a-fdbf-4b6f-b02d-2fc9c2648a18/600x900",
    //    Description =
    //        "В первом и последнем плавании шикарного «Титаника» встречаются двое. Пассажир нижней палубы Джек выиграл билет в карты, а богатая наследница Роза отправляется в Америку, чтобы выйти замуж по расчёту. Чувства молодых людей только успевают расцвести, и даже не классовые различия создадут испытания влюблённым, а айсберг, вставший на пути считавшегося непотопляемым лайнера.",
    //    ReleaseFilmDate = 1997,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Джэймс Кэмерон",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/Qwx0AsI6Zq0?si=ke5sMGLpYo1QMHdm"
    //};
    //film2.Links.Add(@"https://my.mail.ru/bk/wowa5997/video/_myvideo/332.html");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("Без рекламы");
    //film2.Links.Add(@"https://wax.www-lord.stream/318-film-titanik.html");
    //film2.Price.Add("Беслатно");
    //film2.Advertisement.Add("С рекламой");
    //Film film3 = new Film()
    //{
    //    Name = "Игра в кальмара",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/1073b002-96de-49ae-a432-1da460f6890c/600x900",
    //    Description =
    //    "Сон Ги-хун уже немолод, разведён, по уши погряз в долгах и сидит на шее у старенькой матери. Даже выигранные на скачках деньги в его руках долго не задерживаются, и однажды он встречает в метро загадочного незнакомца, который сначала предлагает сыграть в детскую игру, а затем вручает Ги-хуну немалую сумму и визитку. Но радость мужчины сменятся отчаянием, когда он узнаёт, что бывшая жена с новым мужем собираются увезти его дочь в Америку. Он звонит по номеру с визитки и становится последним участником тайных игр на выживание с призом в 40 миллионов долларов. Среди товарищей по несчастью оказываются его друг детства — прогоревший финансист, бандит, смертельно больной старик, северокорейская перебежчица, иммигрант из Пакистана и многие другие отчаянно нуждающиеся в деньгах.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Thriller,
    //    Director = "Хван Дон-хёк",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/ef0IPK9lljw?si=TnrxVXTsRmr9mZcw"
    //};
    //film3.Links.Add(@"https://my.mail.ru/mail/oks.petra/video/Igra_v_kalmara/1081.html");
    //film3.Price.Add("Беслатно");
    //film3.Advertisement.Add("Без рекламы");
    //film3.Links.Add(@"https://20lordserial.xyz/429-igra-v-kalmara-sv-29.html");
    //film3.Price.Add("Беслатно");
    //film3.Advertisement.Add("С рекламой");
    //Film film4 = new Film()
    //{
    //    Name = "Истинная красота",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/22f75234-c22e-43df-8e03-11a7eff64a4e/600x900",
    //    Description =
    //        "Старшеклассница Лим Джу-гён с комплексом по поводу своей внешности привыкла краситься и достигла в этом деле определённого мастерства. Она начинает встречаться с двумя самыми видными парнями.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Melodrama,
    //    Director = "Ким Сан-хёп",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/BhP1eYQ5Pxk?si=3qRZ1HOLgMVBhPAP"
    //};
    //film4.Links.Add(@"https://tv.lordserialu3.online/5253-istinnaja-krasota2f5.html");
    //film4.Price.Add("Бесплатно");
    //film4.Advertisement.Add("С рекламой");
    //film4.Links.Add(@"https://www.kinopoisk.ru/series/1398965/");
    //film4.Price.Add("Платно");
    //film4.Advertisement.Add("Без рекламы");


    //Film film5 = new Film()
    //{
    //    Name = "Счастье",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4774061/9898b051-a211-47ce-846d-728ca83c1c00/600x900",
    //    Description =
    //    "Уже несколько лет человечество живёт в условиях пандемии COVID-19, когда внезапно некоторые начинают испытывать нестерпимую жажду, превращаться в зомбиподобных существ и бросаться на людей. С одним из заболевших сталкивается Юн Сэ-бом — боевая девушка из отдела по борьбе с терроризмом — и, получив от него царапину, отправляется в кризисный центр по борьбе с новой заразой. Обследование не выявляет у неё заражения, и Сэ-бом извлекает выгоду из сложившейся ситуации — заключив фиктивный брак с лучшим другом, принципиальным полицейским Чон И-хёном, получает квартиру в новом доме.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Ан Гиль-хо",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/IwWDhzw5CtQ?si=e8Akwi7VdFBVtn5k"
    //};
    //film5.Links.Add(@"https://m.my.mail.ru/my/welcome?back_after_reg=https://m.my.mail.ru/bk/anveela/video/937");
    //film5.Price.Add("Бесплатно");
    //film5.Advertisement.Add("Без рекламы");
    //film5.Links.Add(@"https://www.kinopoisk.ru/series/4402867/");
    //film5.Price.Add("Платно");
    //film5.Advertisement.Add("Без рекламы");
    //Film film6 = new Film()
    //{
    //    Name = "Вокруг света за 80 дней",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/cefc0812-8400-4e1e-9a04-3bdd0f74981a/600x900",
    //    Description =
    //        "Скучающий джентльмен, спровоцированный друзьями на пари, обещает за 80 дней осуществить кругосветное путешествие. Сообразительный дворецкий и взбалмошная журналистка отправляются с ним за компанию.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Comedy,
    //    Director = "Стив Бэррон, Брайан Келли, Чарльз Бисон",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/RUpfhB-7cBU?si=GMiDmREgVd2xeVuR"
    //};
    //film6.Links.Add(@"https://premier.one/show/vokrug-sveta-za-80-dnej");
    //film6.Price.Add("Платно");
    //film6.Advertisement.Add("Без рекламы");
    //film6.Links.Add(@"https://www.kinopoisk.ru/series/1337642/");
    //film6.Price.Add("Платно");
    //film6.Advertisement.Add("Без рекламы");
    //Film film7 = new Film()
    //{
    //    Name = "Дом дракона",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/4939065f-efa2-4192-a3c2-cec534e79e01/600x900",
    //    Description =
    //    "Таргариены сражаются друг с другом и растят драконов. Эпическая сага о жестокой войне за Железный трон.",
    //    ReleaseFilmDate = 2022,
    //    Type = TypeFilm.Fantasy,
    //    Director = "Клер Килнер, Гита Васант Пател, Мигель Сапочник",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/5weHputuO4g?si=uc4Alqasy2fTVmfW"
    //};
    //film7.Links.Add(@"https://tv3.lordfilms4.online/715-dom-drakona-47e.html");
    //film7.Price.Add("Бесплатно");
    //film7.Advertisement.Add("С рекламой");
    //film7.Links.Add(@"https://www.kinopoisk.ru/series/1316601/ ");
    //film7.Price.Add("Платно");
    //film7.Advertisement.Add("Без рекламы");
    //Film film8 = new Film()
    //{
    //    Name = "Мышь",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/10809116/70aac55f-cd33-4e69-94c1-efd47405805a/600x900",
    //    Description =
    //        "Серийный убийца по прозвищу «Охотник за головами» держал в страхе всю страну. Паника и недовольство силами полиции достигли такого уровня, что правительство всерьёз обсуждало законопроект о генетическом тесте, позволяющем с точностью до 99% определить ген психопатии у эмбриона. Прошло 10 лет, но столь жуткие события не могут исчезнуть без следа и уже тесно переплели судьбы опытного детектива, талантливой женщины-продюсера, рядового полицейского и проблемной старшеклассницы. Но они об этом ещё не догадываются.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Thriller,
    //    Director = "Кан Чхор-у, Чхве Джун-бэ",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/S6CAz71l-IM?si=1PTWLfYIRST6PdFS"
    //};
    //film8.Links.Add(@"https://m.my.mail.ru/my/welcome?back_after_reg=https://m.my.mail.ru/bk/anveela/video/716");
    //film8.Price.Add("Бесплатно");
    //film8.Advertisement.Add("Без рекламы");
    //film8.Links.Add(@"https://www.kinopoisk.ru/series/1387128/");
    //film8.Price.Add("Платно");
    //film8.Advertisement.Add("Без рекламы");
    //db.Film.AddRange(film1, film2, film3, film4, film5, film6, film7, film8);
    //db.SaveChanges();

//}


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSession();

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
