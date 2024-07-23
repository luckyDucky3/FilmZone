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
//using (ApplicationDbContext db = new ApplicationDbContext()) //���������� ������
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
    //    Name = "�������� �����",
    //    Description = "������� ������������ ��������� ������ �� ������ ����. �������������� ���������� � ����� ������� � ����� ����� � ���, ��� ���-�� ������������� �� ������ ����" +
    //                  "���, �������� � ���� ����������� �������� � ���������� ����������� ��� �������, ��������� ���� ����� � ����� ������.",
    //    PathToImage = "~/img/CloudAtlas.png",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Fantasy,
    //    Director = "���� ��������, ��� ������, ����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = "https://www.youtube.com/embed/K2VtiZSvwuo?si=edMZkW70pSaRGQOl",
    //};
    //viewModel.Links.Add("https://www.kinopoisk.ru/film/464484/");
    //viewModel.Price.Add("������");
    //viewModel.Advertisement.Add("��� �������");
    //viewModel.Links.Add("https://okko.tv/movie/cloud-atlas");
    //viewModel.Price.Add("������");
    //viewModel.Advertisement.Add("��� �������");
    //viewModel.Links.Add("https://vk.com/video-220018529_456240240");
    //viewModel.Price.Add("��������");
    //viewModel.Advertisement.Add("� ��������");
    //viewModel.Links.Add("https://ru.lordsfilm.space/333-oblachnyj-atlas.html");
    //viewModel.Price.Add("��������");
    //viewModel.Advertisement.Add("� ��������");
    //Film film1 = new Film()
    //{
    //    Name = "����������� ������",
    //    PathToImage = @"~/img/silicon.png",
    //    Description =
    //        "������� � ������ �����, ��������� � ������� ����������� �������� � ������������������� ������ ���-���������. ������� ����� ������� ��������� " +
    //        "��������� � ���� �������� ����������, �� ������ �� ������� ������ �� 10% ������� �� ������� ��������.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Comedy,
    //    Director = "���� �����, ���� ����, ����� ������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/BbyByc47qno?si=Q1Bjzfw9p4mckGdt"
    //};
    //film1.Links.Add(@"https://hd.lordserialx.online/426-silikonovaja-dolina-2014.html");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("C ��������");
    //film1.Links.Add(@"https://vk.com/video-182169551_456256578");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("C ��������");
    //film1.Links.Add(@"https://silicon-valley-kubik.net/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("C ��������");
    //film1.Links.Add(@"https://www.ivi.ru/watch/silikonovaya-dolina");
    //film1.Price.Add("������");
    //film1.Advertisement.Add("��� �������");
    //    Film film2 = new Film()
    //    {
    //        Name = "��������� ���������",
    //        PathToImage = @"~/img/xfiles.png",
    //        Description = "������� ��� ���� ������ � ����� ������� �������� ������ ��� �������� ���������� ����������. ��� ����� ����������� ���, ���������" +
    //                      " � ��������������� ���������. ������ ����� � ���������� � �������� ������� �������� ������, ��� �� �� �������� ��������� ����������." +
    //                      " ���������� �������� ��������� ����������� � ������ � ���� � ����� �������� �������.",
    //        ReleaseFilmDate = 1993,
    //        Type = TypeFilm.Fantasy,
    //        Director = "��� �������, ��� ������, ����� ������",
    //        FilmOrSerial = FilmOrSerial.Serial,
    //        Preview = @"https://www.youtube.com/embed/Xcf44Nit7_A?si=-Qkq2w0K-a_Rmew0",
    //    };
    //    film2.Links.Add(@"https://rutube.ru/plst/351666/");
    //    film2.Price.Add("���������");
    //    film2.Advertisement.Add("C ��������");
    //    film2.Links.Add(@"https://vk.com/video/playlist/-221125343_172");
    //    film2.Price.Add("���������");
    //    film2.Advertisement.Add("C ��������");
    //    film2.Links.Add(@"https://xfiles.top/seasons?utm_referrer=https%3A%2F%2Fyandex.ru%2F");
    //    film2.Price.Add("���������");
    //    film2.Advertisement.Add("C ��������");
    //    film2.Links.Add(@"https://9lordserial.art/289-sekretnye-materialy-11-sezon-v186.html");
    //    film2.Price.Add("���������");
    //    film2.Advertisement.Add("C ��������");

    //Film film3 = new Film()
    //{
    //    Name = "�������: ���������",
    //    PathToImage = @"~/img/matrix.png",
    //    Description = "���� ����� ����� �������� ���������� ����, ��� ������ �� ��������� ��� ������ �������",
    //    ReleaseFilmDate = 2003,
    //    Type = TypeFilm.Fantasy,
    //    Director = "���� ��������, ����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/KjKQGoZDiq4",
    //};
    //film3.Links.Add(@"https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/?t=5");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("C ��������");
    //film3.Links.Add(@"https://my.mail.ru/mail/votvamby68/video/_myvideo/3871.html?from=videoplayer");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("��� �������");
    //film3.Links.Add(@"https://vk.com/video-127060916_456239110");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("C ��������");
    //film3.Links.Add(@"https://matrix-lordfilm.com/film/matritsa-3-revolyutsiya");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("C ��������");
    //Film film4 = new Film()
    //{
    //    Name = "�������� ����",
    //    PathToImage = @"~/img/max.png",
    //    Description = "� �������� ������� ����� ������� ���������� ��� ����� ��������������� ����� ������������ �����������. ����� ��������, ����� " +
    //                  "������������ �� ������� ��������, ���������� �������� ������������ �����. ������� �� ����� ���������� ������ ���� �����, � ������ " +
    //                  "��� �� ������ ������ ������ ����� � ��� �����.",
    //    ReleaseFilmDate = 2015,
    //    Type = TypeFilm.Military,
    //    Director = "���� ��������, ����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/-3ZoAp6owdk?si=OU1cF9sMIrz395p3",
    //};
    //film4.Links.Add(@"https://vk.com/video-176294899_456240989");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("C ��������");
    //film4.Links.Add(@"https://ok.ru/video/1792787548709");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"https://hdf4im.kinolord6.pics/7267-bezumnyj-maks-1979.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("C ��������");
    //film4.Links.Add(@"https://my.mail.ru/mail/asps11/video/297/8809.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");
    //Film film5 = new Film()
    //{
    //    Name = "�� ��� ������",
    //    PathToImage = @"~/img/breakingbad.png",
    //    Description = "�������� ������� ����� ������ ���� �����, ��� ����� ����� �����. " +
    //                  "�������� ������� ���������� ��������� ��� �����, � ����� �����������, ������ ������ �������� ������������� �������������. " +
    //                  "��� ����� �� ���������� ������ ������� ������� ������ ��������, �����-�� ������������ �� ����� ��� �������� ���������� �����. " +
    //                  "������� ��� ��������� ������ ����, �� ��������, � ���� ����� ���, �� ������� ���������� � �����������.",
    //    ReleaseFilmDate = 2008,
    //    Type = TypeFilm.Criminal,
    //    Director = "������ ��������, ���� ���������, ���� ��������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/OpcX_Q0PGM4?si=XwXDXAfWWpq05ZSh",
    //};
    //film5.Links.Add(@"https://breakingbad.smotret-online.ru/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("C ��������");
    //film5.Links.Add(@"https://rutube.ru/plst/362164/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("C ��������");
    //film5.Links.Add(@"https://breaking.smotrim-smotrim.ru/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("C ��������");
    //film5.Links.Add(@"https://9lordserial.art/29-vo-vse-tiazkie-5-sezon-v186.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("C ��������");
    //Film film6 = new Film()
    //{
    //    Name = "������� ��������",
    //    PathToImage = @"~/img/dead.png",
    //    Description = "�����-�������� ����������� �������. ����� ��� ������ ������������ � ������ � ��������� ������� �������� � ������� ����������� " +
    //                  "�����. �� ���������� ����� ������ ������ ���� �������� ������� ������, ��������� ��������� �� ��������� ����������� ������� �����" +
    //                  "������� ����������. ��� �������� ������ ������� � ��������, ��� �������������� ����� ����� ����� ���� ������� ������� ���������.",
    //    ReleaseFilmDate = 2010,
    //    Type = TypeFilm.Horrors,
    //    Director = "���� ��������, ����� �. ����������, ����� ����",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/0wz2uCXg9aw?si=aj1ToUUbFaWrcnkY",
    //};
    //film6.Links.Add(@"https://lordserialk7.top/zarubezhnyi/34-hodjachie-mertvecy_v3.html");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("C ��������");
    //film6.Links.Add(@"https://vk.com/video-80021931_456239833");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("C ��������");
    //film6.Links.Add(@"https://18lordserial.xyz/462-xodiacie-mertvecy-sv-29.html");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("C ��������");
    //film6.Links.Add(@"https://walking-dead.homes/?utm_referrer=https%3A%2F%2Fyandex.ru%2F");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("C ��������");
    //Film film1 = new Film()
    //{
    //    Name = "�������",
    //    PathToImage = @"http://www.prdisk.ru/images/trigger_dvd_16_serijj._5_dvd_r_f3d_big.jpeg",
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
    //Film film2 = new Film()
    //{
    //    Name = "��� � �� �������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/2/25/1981_vam_i_ne_snilos.jpg/184px-1981_vam_i_ne_snilos.jpg",
    //    Description =
    //        "�������� �������� �������� ���������� ���� ����������. �������, ������������ ������ ���� � ������ �����",
    //    ReleaseFilmDate = 1980,
    //    Type = TypeFilm.Melodrama,
    //    Director = "���� ����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/aBiPSjBs7XI?si=EB3_WwXU03gEC-UX"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/45660/?utm_referrer=www.google.com");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://www.youtube.com/embed/K7nTbO8kMAo?si=j5JTv3uaMiHfdUoY");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://www.ivi.ru/watch/51623");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("��� �������");

    //Film film1 = new Film()
    //{
    //    Name = "�������� ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/c11652e8-653b-47c1-8e72-1552399a775b/220x330",
    //    Description =
    //        "������������ ����, ������������ � ���-�������� ����������� ��������� ����� ��������. ����� ���������� ������ 1945-1955 �����. ����� �����, ��� ���� ��������, ����� ����� ���� ����. � ��� ����� �� ������ ������� ����� ������������ ��� ������� ��� �����. �����, ����� �����, �������� �����, �� �������� ������� �������� �������� �������� ��������. ��� �������� ���� ���� �� ������ ��������, �� ��������� ���� �������, � ���������� ����, �������� �������� ����������� �������. �� ���� �������� ����������� ���������.",
    //    ReleaseFilmDate = 1972,
    //    Type = TypeFilm.Criminal,
    //    Director = "������� ���� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/ar1SHxgeZUc?si=A79z5KZMg9cbeLbO"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/39568c110b232ad19985fb3a339f486d/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://godfather-film.ru/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("� ��������");
    //film1.Links.Add(@"https://www.culture.ru/live/movies/352/a-zori-zdes-tikhie");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //Film film2 = new Film()
    //{
    //    Name = "1+1",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b9/Intouchables.jpg",
    //    Description =
    //        "��������� � ���������� ����������� ������, ������� ���������� ������ �������� � ��������� ��������, ������� ����� ����� �������� ��� ���� ������, � �������� ������ ���������� ������, ������ ��� ��������������� �� ������. �������� �� ��, ��� ������ �������� � ����������� ������, ������ ������� ��������� � ����������� ����� ����������� ��� �����������.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Drama,
    //    Director = "������ �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/tTwFeGArcrs?si=3aZ1Puut2IAKOCID"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/535341/");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://okko.tv/movie/intouchables");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://rutube.ru/video/50d09cd2daa46a98172b8b7bb6846271/");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("� ��������");

    //Film film1 = new Film()
    //{
    //    Name = "��������� �������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b1/%D0%9C%D0%B0%D0%BB%D0%B5%D0%BD%D1%8C%D0%BA%D0%B8%D0%B5_%D0%B6%D0%B5%D0%BD%D1%89%D0%B8%D0%BD%D1%8B_%28%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%2C_2019%29.jpg",
    //    Description =
    //    "������� ���������� ������ ��������� ���� �� ����� ������. ���-�� ������ ����������� �����, �� ��������, � �������� ������������ �������, ��������� ��� �������: ������ ������, ������� �������������, ����������� ������� � ��������� ������ ���� � ������ ����� � �����.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/SnwKCS6kkvE?si=5RohyAKKrBnn0I_Z"
    //};
    //film1.Links.Add(@"https://kion.ru/video/movie/155747128");
    //film1.Price.Add("������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://bridzhit-dzhons.ru/malenkie-zhenschiny-2019/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("� ��������");
    //Film film2 = new Film()
    //{
    //    Name = "�������� � �������������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/8/86/Pride_%26_Prejudice_2005.jpg",
    //    Description =
    //        "������, ����� XVIII ����. �������� ������� ������ ������ ��������� ���, ����� ������ ������ ������� �����. � ������ ����������� ����� ��������� ��������� ���������������� ����� ����, ����� �� ��������� ���������� ������� ���������� - ������ ������...",
    //    ReleaseFilmDate = 2005,
    //    Type = TypeFilm.Melodrama,
    //    Director = "��� ����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/kIwUj0X25ho?si=i3UPeAQ30ZKOuuPE"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/81733/");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://pride-prejudice.ru/1-2005/");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("��� �������");

    //Film film3 = new Film()
    //{
    //    Name = "������� � ��������� ������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1704946/84be7629-189d-4b13-8b35-fcf010512218/600x900",
    //    Description =
    //    "�������, ������������ �� ����� ������ ������� ����� � ���������� ������ ����� ��������� � ������ �� �������������� � ������������ ��������" +
    //    " �����, ������������� ���� ���������� ����������������� ������. ��� ��������� ���������� � ������ � ��������� ��������� �� ������ ������� ������ ������, " +
    //    "� �������� �����, �������� � ����� ��������������� � �������������� ������������.",
    //    ReleaseFilmDate = 2008,
    //    Type = TypeFilm.Drama,
    //    Director = "���� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/w_o1RQKbl6Q?si=tZHiskOYeGtfS6R4"
    //};
    //film3.Links.Add(@"https://rutube.ru/video/8a8ff15b51c0a2f8ac83809f30b2549e/");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("� ��������");
    //film3.Links.Add(@"https://bogmedia.org/videos/1359/malchik-v-polosatoy-pijame/");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("� ��������");

    //Film film4 = new Film()
    //{
    //    Name = "������ ��������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/3/38/Schindler%27s_List_movie.jpg",
    //    Description =
    //        "����� ������������ �������� ������� ����������� ������ ��������, ����� ���������� ������, �������������� ����������, �������� �� ����� ������ ������� " +
    //        "����� ����� 1200 ������.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Biography,
    //    Director = "������ ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/4r2Z0U9Y53o?si=uYt_ANH2RXHjpUdj"
    //};
    //film4.Links.Add(@"https://lordserials.cx/2983-schindlers-list.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("� ��������");
    //film4.Links.Add(@"https://www.ivi.ru/watch/100131");
    //film4.Price.Add("������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/b/b3/%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%D0%9E%D1%81%D1%82%D1%80%D0%BE%D0%B2.jpg/233px-%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%D0%9E%D1%81%D1%82%D1%80%D0%BE%D0%B2.jpg",
    //    Description =
    //    "������ ������� �����. �����, �� ������� �������� � ��� ������� ������� ����� ��������� �����, ����������� �������� ���������� �������. ��������� ������ � ������, �������� ��������� ������������� � ������������� ������. ����� ��������� ����� �� �������������� �����, �� ��������� ������ �������, ����������� � ��������� �� �������, ��� ������� ������.",
    //    ReleaseFilmDate = 2006,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/mMe_VJTz33M?si=seoOC6MGSs48Z6lI"
    //};
    //film5.Links.Add(@"https://www.youtube.com/watch?v=qa_13X32DRg");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://rutube.ru/video/220d7ff7bc46da403af8ccff51e9a911/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //Film film6 = new Film()
    //{
    //    Name = "�����������",
    //    PathToImage = @"https://i.bigenc.ru/resizer/resize?sign=cLBJ1TTubCSxDUqvyf90zA&filename=vault/536d0926c893ab0c48dacceca10a06a8.webp&width=1200",
    //    Description =
    //        "������� � ������ ������� ������� � ������� ����������� ����� ������������, ������� �������� ����� ������.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Melodrama,
    //    Director = "��� ������, ����� ����, ���� ������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/Cc6Z9TEHXV8?si=xpCSh_OwZ_vgIKMx"
    //};
    //film6.Links.Add(@"https://bridgerton.ru/");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("� ��������");
    //film6.Links.Add(@"https://lordserials.fan/zarubezhnye-serialy/271-bridzhertony-v234.html");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("� ��������");

    //Film film7 = new Film()
    //{
    //    Name = "�������� ���",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/8472ca52-2751-4bbe-9a08-8a1be75f93d5/220x330",
    //    Description =
    //    "������� � ������������, �������� �������� ����������� �������� ���� ������� � ������� 2,4 ���� ����.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.Detective,
    //    Director = "����� ���������, ����� �������, ������ �����",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/-ysaahmRuaA?si=S6X4RMLvcPDBgKYo"
    //};
    //film7.Links.Add(@"https://paper-house-tv.com/season-1/4-1-sezon-1-serija-soglasovannye-deistvija.html");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");
    //film7.Links.Add(@"https://lordserials.cx/1748-la-casa-de-papel.html");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");

    //Film film8 = new Film()
    //{
    //    Name = "��� �������� ��������� �� ��������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1900788/bd6e89fb-0ee3-415f-9385-6c1a358dca57/600x900",
    //    Description =
    //        "��������� ������� ������ � ��������� �����������. ��� ��������� ��������� ���������� ��� ��������� ���� �������� ��������� �� ��������." +
    //        " �� �� ���� �� �����������, ��� � ������ ������� �� �������� ��������� ���� ������ � �������� �����.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Detective,
    //    Director = "���� Ē����, ������ �����, ���� �����",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/hAgggcrAjEU?si=SRmfYHb9EY-g4sPB"
    //};
    //film8.Links.Add(@"https://vk.com/video-105813732_456242902");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("� ��������");

    //Film film1 = new Film()
    //{
    //    Name = "����������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/6/6f/%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D1%84%D0%B8%D0%BB%D1%8C%D0%BC%D0%B0_%C2%AB%D0%A2%D0%B5%D1%80%D1%80%D0%B8%D1%82%D0%BE%D1%80%D0%B8%D1%8F%C2%BB.jpg",
    //    Description =
    //    "���������� - ��� �����, ��� ���� ����������� �� ���������. ����������� ������������, ��� ������ ����������� � �������� �������� ���������� ������. ������� " +
    //    "������� �����, �������� �������� ������� ����� ��������. ������ ���� ������, ��������� ����� ����� ����������� ������ ����������, �������� ������� ����������, " +
    //    "������� ��������� �� ����� ���, ������� ����������� �����.",
    //    ReleaseFilmDate = 2015,
    //    Type = TypeFilm.Drama,
    //    Director = "��������� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/a4N57w1nbZc?si=2zcQxS-xaaEl_jaK"
    //};
    //film1.Links.Add(@"https://www.kinopoisk.ru/film/601933/");
    //film1.Price.Add("������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://www.ivi.ru/watch/126206");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("� ��������");
    //Film film2 = new Film()
    //{
    //    Name = "������� ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/3560b757-9b95-45ec-af8c-623972370f9d/600x900",
    //    Description =
    //        "���� �� ���������� ���������, ������� ���� � �� ����� �����, �� ������ � �������� ������ � ������������ ��������� ��������� ������� ����� �������������� �����.",
    //    ReleaseFilmDate = 1994,
    //    Type = TypeFilm.Comedy,
    //    Director = "������ �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/otmeAaifX04?si=O8u8pXgpMtT_Rcmb"
    //};
    //film2.Links.Add(@"https://kion.ru/video/movie/118278367");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://vk.com/video-223157201_456239063");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("��� �������");

    //Film film3 = new Film()
    //{
    //    Name = "����������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/c/ca/Terminator_poster.jpg",
    //    Description =
    //    "������� �������������� ������� ����� ���� � �������-�����������, ��������� � 1984 ��� �� ����-����������������� ��������, ��� ����� ������ ������-������, � ������������ ��������� �� ����� ���������.",
    //    ReleaseFilmDate = 1984,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "������ �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/uA4k5Vc5jFc?si=4_7PhSWrIljmndj8"
    //};
    //film3.Links.Add(@"https://www.youtube.com/watch?v=iHD0JXpXKTQ");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("��� �������");
    //film3.Links.Add(@"https://rutube.ru/video/e64f1986c449d2ab5b58ecab25c16b16/");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("��� �������");

    //Film film4 = new Film()
    //{
    //    Name = "������� �����. ������ 1: ������� ������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/4/40/Star_Wars_Phantom_Menace_poster.jpg",
    //    Description =
    //        "������ � ������������ ������� ����. �������� ���������, �� ����� ������� ������, �������� � ������ �������� � ��������� ��������, �������� �� �������, ��� �������� � �����. �� ������� �������� � ���������� � ��� ��������� ��� ������-������: ������� � ������, ����-���-���� � ���-��� ������...",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Fantasy,
    //    Director = "������ �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/omuTFYiVqgA?si=-re_BoAfSWrk_QE-"
    //};
    //film4.Links.Add(@"https://rutube.ru/video/056125631a5a264ca7f40ec7fedb7519/");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"https://star-wars-films.ru/");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "������� �����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/5/54/Rain_man.jpg",
    //    Description =
    //    "����������� � ������������ �������� �������� ����� � ���������� �� ���� ��������� ���� ������� ����� �� ������ 1949 ����, � ������� ���� ���������� ������ ��� �����-������� ��������. ��������� ����� �������� ����� �����, ����� �������� �������� �����. �� ����� ��������, ��� ������� �������� �������� ��������������� �������������, ������� � ���������������, ������ ������������ ��� � ��������� �����.",
    //    ReleaseFilmDate = 1988,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/1AnTSk9YzKk?si=cDYspwq4xFyhEivV"
    //};
    //film5.Links.Add(@"https://rutube.ru/video/73bdc04748f4a38563e98677d448dbd0/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://www.hdfilmfinest.com/load/dramy/25110-chelovek-dozhdya-1988.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("� ��������");
    //Film film6 = new Film()
    //{
    //    Name = "���� �����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/4/41/Groundhog_Day.jpg",
    //    Description =
    //        "������������� ����������� ��� ������� ������ ��� ��������� � ��������� ������� � ����� ������������ �� ������������ ��� �����. �� �� ���� ��� ������� ������� ����� ������� ������. ����� ������� � ��� ���� �����: ��� ����� �� � ������������.",
    //    ReleaseFilmDate = 1993,
    //    Type = TypeFilm.Comedy,
    //    Director = "������� �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/5Fsr_cJxag0?si=1FIHAutgsM7iPjz-"
    //};
    //film6.Links.Add(@"https://rutube.ru/video/7137fdcf6c797b32d455cc512a02a23b/");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("��� �������");
    //film6.Links.Add(@"https://www.hdfilmfinest.com/load/fehntezi/25682-den-surka.html");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("� ��������");

    //Film film7 = new Film()
    //{
    //    Name = "�������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4774061/cf1970bc-3f08-4e0e-a095-2fb57c3aa7c6/600x900",
    //    Description =
    //    "����� ������ ��������� ��������� �� ��� �����: ��� �� � ����� ������� ������� ��������, ���������� ������� �� ����������, � ����� ������������ � ������ �� ����� ���, � ��� ����� � ����, ���� �� �� �� ���� ����������. �� ������� �� ��������. ����� ����� ��������� ������ � ����������.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "���� ��������, ����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/YihPA42fdQ8?si=0v7IJuowJcQHfhfv"
    //};
    //film7.Links.Add(@"https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");
    //film7.Links.Add(@"https://matrix-films.ru/");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");

    //Film film8 = new Film()
    //{
    //    Name = "����� � �������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/9/90/BTTF_DVD_rus.jpg",
    //    Description =
    //        "��������� ����� � ������� ������ �������, ���������� ��� ������-����������� ����� �������, �������� �� 80-� � ������� 50-�. ��� �� ����������� �� ������ " +
    //        "�������� ����������, ��� �����������, � ������-�����������, ������ �������.",
    //    ReleaseFilmDate = 1985,
    //    Type = TypeFilm.Fantasy,
    //    Director = "������ �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/ou8w0gQHlRE?si=jKRGFj_H0NutC33k"
    //};
    //film8.Links.Add(@"https://hd3.2-vse-chasti-filmov.xyz/34-nazad-v-budushee.html");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("� ��������");
    //film8.Links.Add(@"https://www.ivi.ru/watch/90324");
    //film8.Price.Add("������");
    //film8.Advertisement.Add("��� �������");


    //Film film1 = new Film()
    //{
    //    Name = "������� �� ������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/9/9b/%D0%91%D0%B5%D0%B3%D1%83%D1%89%D0%B8%D0%B9_%D0%BF%D0%BE_%D0%BB%D0%B5%D0%B7%D0%B2%D0%B8%D1%8E_2049.jpg",
    //    Description =
    //    "� ��������� ������� ��� ������� ������ � ������������, ���������� ��������� ����� ������� ������. ������ ������� ������� ��� � ������� ����������� ��� ��������� � �������� ������������ ����������. �� �������� ���������� ����������� ��������� ����������, ������� ������ ��� ������ ������������� ����� ������������. ����� ����� ���� � ��������, ��� ������ ��������� ���� ������� � ������� ������� ������������ ������������� ������� ���-���������, ������� ��������� ����� ����� ��� �����.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "���� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/taQW31SVPCk?si=wtWT-4niissleLmj"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/fbd9299eca041a8260dcb83394271af0/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://kion.ru/video/movie/113571977");
    //film1.Price.Add("������");
    //film1.Advertisement.Add("��� �������");
    //Film film2 = new Film()
    //{
    //    Name = "��������� �����: �������� ������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/0/08/The_Lord_of_the_Rings._The_Fellowship_of_the_Ring_%E2%80%94_movie.jpg",
    //    Description =
    //        "����� �������, ��� ����� �������. ����� �� 111-� ���� �������� � ������ ������� ����� ������ ��������, ��������� �������� �������� ����� �������� � ������, ������� ������ ����� ����� ��� �����. ��� ������ ������������ �����-�� ������� ���������� ���������� �������, � ��� ���� ������� ������ ������ ����������. ������ ������ ����� ������� ���� ������ ��� �����������. ������ ������ ������ ���������� �����, ����� ��� ���� ��� � ������� ���� � ���������.",
    //    ReleaseFilmDate = 2001,
    //    Type = TypeFilm.Adventures,
    //    Director = "����� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/RNksw9VU2BQ?si=0hF2kbbNe_IDMuzG"
    //};
    //film2.Links.Add(@"https://rutube.ru/video/6a20ed30f8b1368f21d36605b43f8ba0/");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://lord-rings-lordfilm.cam/vlastelin-kolets-bratstvo-koltsa");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("� ��������");

    //Film film3 = new Film()
    //{
    //    Name = "�������� �����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/9c655ce1-2b59-4941-97aa-f9e928ed0ff4/600x900",
    //    Description =
    //    "�������� �������� � ������� ������� ������ �� ����� �������� ������. ���, ���������, ��� ��� ������������ ��������� ����� � ��� �� ���������, �������� ������ �������� �������� ����������� � �����������-�������� ���������� ��������, ������� ��� �� ������ ��������� ��������������� ������� ������. ��� ������ �������� ��������� �� �������� � �����������. �� �������� ������ �������� ���� � ��� ������, ���� ��� ��������� ��� ������� ����������� ������������� ����� ������ �����.",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Horrors,
    //    Director = "�������� �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/9JTNhAOVosk?si=09KUt7Fge6fKAf1a"
    //};
    //film3.Links.Add(@"https://rutube.ru/video/a4e9818b98f7057404c538d1807e7629/");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("��� �������");
    //film3.Links.Add(@"https://lordserials.cx/2990-the-silence-of-the-lambs.html");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("��� �������");

    //Film film4 = new Film()
    //{
    //    Name = "������� � ���������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/d7ad29b7-da97-42b4-9669-c14a1a822591/600x900",
    //    Description =
    //        "������� ����� � ��������� �����, ������� ����������� � �����, �� ������ �� ������, ����� ������ �����. �� ����������� ����� ������ ����������, ����������� �������� � ��������� ������������. ��� � 30 ���� ��������� ����� �������. ������ ����� ��������� � ������� ��� ��� ����. ��� �������� ���, ��� ������� ��������� �� �����, � �������� ����� ����� �� ���������, ����������� �� ����� ����������. �� ������� ���������� ������� � ��������� ����...",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Fantasy,
    //    Director = "��� ����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/lgqluXtxL9Y?si=G-SIk9B7Aafb49ed"
    //};
    //film4.Links.Add(@"https://rutube.ru/video/8da63782936497a7bc06d5699df4541e/");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"https://beguschii-v-labirinte.ru/");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "����� �� ��������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/d/de/Movie_poster_the_shawshank_redemption.jpg",
    //    Description =
    //    "��������� ���� ������� ������ � �������� ����������� ���� � � ���������. ���������� � ������ ��� ��������� �������, �� ������������ � ����������� � �����������, �������� �� ��� ������� �������. ������, ��� �������� � ��� �����, ���������� �� ����� �� ����� �����. �� ����, ���������� ����� ���� � ������ �����, ������� ������ ��� � �����������, ��� � � ����������, ��������� �� ������� � ���� ������������.",
    //    ReleaseFilmDate = 1994,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/kgAeKpAPOYk?si=tyvH-TJ4HS-KYAa1"
    //};
    //film5.Links.Add(@"https://rutube.ru/video/cae96c24f81419ccd444ed151a169985/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://lordserials.cx/2970-the-shawshank-redemption.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("� ��������");
    //Film film6 = new Film()
    //{
    //    Name = "����� �����",
    //    PathToImage = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQP8kErB6Gym961lBx1Trsp5nRSWbM68IWPrjqU4cXN5w&s",
    //    Description =
    //        "������, 1884 ���. � ���� ������ 16-����� ����� ����� ������������, ��� � ���� ��������� �������. ����������� �� ������� � ����� ���������� ����� ��� ������� ��������� � �������, ������� �� �������� ��������� ���������. ��������� ���� ���� ����, ���� � ������� ������������� �������� � ����������� � ������� ���� ����, � ������ ���� ����� �� ������������� ������������� ������������� � ��������� ������� �����. ���� ������ � ����������� ������, �������� �������� � �������� �������� � ���������� ��������� � ������� �������. ������ �� � �������� �� ������ �����������, ����� �������� ����������� ����������� ������� ��������� �, ������������ ���������, ������ ������� �� ����� � ������, ��� ���������� � ������� ��������, ���� ��������� �� ����� �����.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Adventures,
    //    Director = "����� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/AVU3SI3Jb5Y?si=dohG6hxcTwanlKk6"
    //};
    //film6.Links.Add(@"https://rutube.ru/video/d4b97faede9fa60d38ad259ee64498cd/");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("��� �������");
    //film6.Links.Add(@"https://zonafilm.ru/movies/enola-holms");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("��� �������");

    //Film film7 = new Film()
    //{
    //    Name = "������� ����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/b/b0/Green_mile_film.jpg",
    //    Description =
    //    "��� ������� � ��������� ����� ���������� � ������ ��������� ����, ������ �� ������� �������� ������� �������� �������� ����� �� ���� � ����� �����. ��� ������� ����� ����������� � ������������ �� ����� ������. ������ ������ ���� �����, ��������� � �������� ������������, ���� ����� �� ����� ��������� ���������� �����.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Fantasy,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/TODt_q-_4C4?si=LLUVL30_D-_DEUXe"
    //};
    //film7.Links.Add(@"https://vk.com/video-220018529_456240901");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");
    //film7.Links.Add(@"https://www.hdfilmfinest.com/load/fehntezi/26066-zelenaya-milya.html");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");

    //Film film8 = new Film()
    //{
    //    Name = "������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/3/3b/The_Fast_and_the_Furious.jpg/212px-The_Fast_and_the_Furious.jpg",
    //    Description =
    //        "��� ����� ������, � �� � ����� ������ � ����������������. �� �������� ������� � ��������� ������������ �������� �������, �������� ������� � ���������� ������� �����. ������ ����� �����������, � ��� ������� � ��������� � ������� � �������, �������������� � ������������ � ������� �������� ����������, ����������� ����� �� ����.",
    //    ReleaseFilmDate = 2001,
    //    Type = TypeFilm.ActionMovie,
    //    Director = "��� ����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/8QyEruRwNeo?si=QI5AKI6RJ7BTI9cu"
    //};
    //film8.Links.Add(@"https://www.kinopoisk.ru/film/666/");
    //film8.Price.Add("������");
    //film8.Advertisement.Add("��� �������");
    //film8.Links.Add(@"https://tv.forsazh-lordfilm.com/115-forsazh.html");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("� ��������");

    //Film film1 = new Film()
    //{
    //    Name = "���� ������ ����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/d/d3/I%2C_Tonya.jpg",
    //    Description =
    //    "������������ ���������� ���� ������� �������� �������: ������� ������� ������� � ������� �������, ����� ������� ������, ������ ���� � ����������� � ������� �� ������������� ��-�� ���������� ������� ������. � ����� �������� �������: �� ����� ��������� ������������ ���� �������� ����������� ������� ���� �����.",
    //    ReleaseFilmDate = 2017,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/y1Nx9r1rQQQ?si=y1GbN5dagbiDgs9O"
    //};
    //film1.Links.Add(@"https://rutube.ru/video/b0ba205adefeddcb3fe536c20fe417c5/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://www.kinopoisk.ru/film/1008385/");
    //film1.Price.Add("������");
    //film1.Advertisement.Add("��� �������");
    //Film film2 = new Film()
    //{
    //    Name = "��-��-����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/e/e4/La_La_Land.jpg",
    //    Description =
    //        "��� ������� ����� ���������, ������� ����� ��������������� ������ ���� ������������ �����������, � ����������� ��������� ���������, ������������ ������������� � ��������� �����. �� ��������� � ���������� ����� �������� ����������� �� ���������.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Musical,
    //    Director = "������ ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/lneNCBIXD4I?si=bAoodZAl_8_Fy8um"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/841081/");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://www.ivi.ru/watch/136478");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("� ��������");

    //Film film3 = new Film()
    //{
    //    Name = "����. �������������. ������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/38839aae-c172-4ead-a082-2847d086cfb3/600x900",
    //    Description =
    //    "����, ��� ���, ��� �� ����� ������, ����� � ��������� ������, ��� � ���, ����������� �������, ������� � ���������, ���������� ��������� � ������� � ��������� �����. ������� � ����� � ��� ���� ��� ���� � �� �������������� ���������� �� ������. ����������� ������, �������������� ������ ������� � ������ ������������� �������� � ��� ��� ����������� ��� �������. ���� ���� ����, ����� ������, �� ���������� ������������ �� ���������.",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Melodrama,
    //    Director = "���� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/5qdS0lfqR7k?si=ukcHBAXU-a1_dMZL"
    //};
    //film3.Links.Add(@"https://new.filmhd1080.sbs/3336-1104-film-leto-odnoklassniki-lyubov-2011-smotret-onlayn.html");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("� ��������");
    //film3.Links.Add(@"https://www.hdfilmfinest.com/load/melodramy/14988-leto-odnoklassniki-lyubov.html");
    //film3.Price.Add("���������");
    //film3.Advertisement.Add("� ��������");

    //Film film4 = new Film()
    //{
    //    Name = "���� ���������",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/thumb/4/49/Game_of_Thrones.jpg/262px-Game_of_Thrones.jpg",
    //    Description =
    //        "� ����� �������� ����� �������������, � ����, ��������� ����� �����������, �������. ������ ���������� ������ ���� ����������, ��������� �����, ����� �������, � � ��� ��������� ����� ������ ������ ������ ��������� � ����� ������ ������� ������. � ����, ��� ��� � �� ������ �� �������� � ������ � ������, ������ ������� � ������ ������� ��� � �����, ���� ����� � ������������, ����������� � �����. ����� ��� ����� �� �������� ����������� ���� �� ������ ������ �� ������ � � ���� ����� �������� ����� � ��� �� ���.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Fantasy,
    //    Director = "����� ������, ���� ������, ����� ������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/edBuDu7QE14?si=czcDkJUTKZ0FOdYw"
    //};
    //film4.Links.Add(@"https://www.kinopoisk.ru/series/464963/");
    //film4.Price.Add("������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"http://thrones-online.com/season-1/4-1-sezon-1-seriya-zima-blizko-1.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("� ��������");


    //Film film5 = new Film()
    //{
    //    Name = "����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/7bbd225f-e6db-4326-b600-1ac294cf9d99/600x900",
    //    Description =
    //    "������ � �������� ������ ���������� ������������� ����� ������� ��������, ��������� �������� ������ ����� ������ ������� �������� ������ ���������� �����.",
    //    ReleaseFilmDate = 2005,
    //    Type = TypeFilm.Comedy,
    //    Director = "��� ���, ������� �������, ��� ������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/6aCZSPJwvbk?si=OwO_q1jZiuLaoXRm"
    //};
    //film5.Links.Add(@"https://officeserial.ru/seasons/season-1/episode-1");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://rutube.ru/video/bd5381d57eea24963dd12f57cdd28dbb/?playlist=338124&playlistPage=1");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //Film film6 = new Film()
    //{
    //    Name = "���� ����",
    //    PathToImage = @"https://upload.wikimedia.org/wikipedia/ru/5/55/Twinpeaks2.jpg",
    //    Description =
    //        "������� ���������� � �������� � ������� ����������� ���� ��������������� ���� ������, ���������� � ���������� � ������������ ������� �� ����� �����. � ���� ������������� ����� ������������� ��������� ������ ������, ������ ������� � ��� ���������� �������� ������ ������ ���� ����. ���������� ������� ��������� ��� ���� ������ � �������� ������� ����� ���������� �� ������ ������ ������ � ������� �������.",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Drama,
    //    Director = "����� ����, ����� ����� �������, ����� ��������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/4-MYnxhcElk?si=DZ17gqHGHCq_R780"
    //};
    //film6.Links.Add(@"https://www.kinopoisk.ru/series/84358/");
    //film6.Price.Add("������");
    //film6.Advertisement.Add("��� �������");
    //film6.Links.Add(@"https://lordserials.cx/491-twin-peaks.html");
    //film6.Price.Add("��������");
    //film6.Advertisement.Add("� ��������");

    //Film film7 = new Film()
    //{
    //    Name = "�������� � �����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/e44eb2d3-9a69-4d03-b851-88f694c4fd55/600x900",
    //    Description =
    //    "��������-������, ����������� ����� �� ������ � ���-��������, ���������� ������ ��������. 48 ���������� ����������� �� ��������� ������� ������� ������. ���� � ������.",
    //    ReleaseFilmDate = 2004,
    //    Type = TypeFilm.Fantasy,
    //    Director = "���� ������, ������ �������, ��� �. �������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/nyXAPpf9Z58?si=TBH-BsB2EtDAT4O_"
    //};
    //film7.Links.Add(@"https://rutube.ru/channel/35825560/");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");
    //film7.Links.Add(@"https://lost-tv.online/episodes/1-sezon-1-seriya/");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");

    //Film film8 = new Film()
    //{
    //    Name = "������ �������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/a3ae9e25-5b10-42f3-96ae-5d707fc6a1bc/600x900",
    //    Description =
    //        "�� ��������� ���� ���������� ����������� �������� ���� �����, ������ ��� �� ������ ���������� � ���������� � ���. � ������ ����, �� ������ �����, �� ������ ������ � ���������� ���������, ������� ����������, ������� ��������� � ������ ������� ������ ������������� � XXI ����.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Drama,
    //    Director = "���� ������, ���� ��������, ������ ����",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/11Dlkoi2u6M?si=W-JSSDCMjAfAfQ7c"
    //};
    //film8.Links.Add(@"https://rutube.ru/channel/31479213/");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("��� �������");
    //film8.Links.Add(@"https://chernoe-zerkalo.ru/1-season/4-e1.html");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("� ��������");


    //Film film1 = new Film()
    //{
    //    Name = "���� ����",
    //    PathToImage = @"https://klike.net/uploads/posts/2020-04/1586848216_4.jpg",
    //    Description =
    //    "������������ ��������� ������������ �� ������ � ������, �� � ������ ������ ����������� �������� �������� ����... ������ �� ����� �����. ���� ��������, ������, �� �������� � ������������� ������ �����������������. � ����� � ��� �������� ���������, �� ���������� �� ��� �������� � ������� � ����� �������. ",
    //    ReleaseFilmDate = 1990,
    //    Type = TypeFilm.Comedy,
    //    Director = "���� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/bBU_64CTNsw?si=ex8YAvKaWHFcNJ8s"
    //};
    //film1.Links.Add(@"https://my.mail.ru/mail/nata.ermolenko.1968/video/_myvideo/218.html");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://hd.odindomalordfilm.ru/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //Film film2 = new Film()
    //{
    //    Name = "����� 2",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4483445/42253d2f-65fc-4f0b-b95d-a8fa9b169502/1920x",
    //    Description =
    //        "�����, ������ �����, ���������� ������� � ������� ���������, ����� ����������� � �������� ����� ��������� �� ����� ����������������. �, �������, �� ����� ������ ���� ����������, ������� ������ ������ � ������������ ����. ������ �������������, ��� ������ ������� ������������ ���� � ������ �������.",
    //    ReleaseFilmDate = 2023,
    //    Type = TypeFilm.Comedy,
    //    Director = "���� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/jmq-JT7Az1w?si=UP0ImWHw4UzCK9KJ"
    //};
    //film2.Links.Add(@"https://www.kinopoisk.ru/film/5047468/");
    //film2.Price.Add("������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://ru-film-v4.lordfilm.limo/222-test-x-m7.html");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("� ��������");
    //Film film3 = new Film()
    //{
    //    Name = "�����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/ea280ff7-1989-44e4-97a8-e2598aa951a4/600x900",
    //    Description =
    //    "27-������ ���������� ����� �������� ������ �� ���������������. ������� ���� ��������� ��� ����������, �� ���, ��� ��� ������� �������� � ������������ ��������� �������, ��� ������ ����������� ��� ����. ����� ��������� ������� �������� ���� ��������, � �� ���������� � ���������, ������������� ������� ������ ����������� �� ���������. \r\n������ ������ �������� � ������ � �������� � ���� �� ����������� �������. �� ����� � ������ 1860 ����, � ��� �� � ���������� ����� ������, �������� �� ����� ����������� � ��������� ������ ������ ������� ������, � �� � �������� �� ������ � ����� �������� ������.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Comedy,
    //    Director = "���� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/C8m6K_Er3BI?si=lJ-5NPGud_-he5PT"
    //};
    //film3.Links.Add(@"https://hd.kinopoisk.ru/film/4cc6fae94f652033b3d127ecbe373205");
    //film3.Price.Add("������");
    //film3.Advertisement.Add("��� �������");
    //film3.Links.Add(@"https://premier.one/show/holop");
    //film3.Price.Add("������");
    //film3.Advertisement.Add("��� �������");
    //Film film4 = new Film()
    //{
    //    Name = "������������� ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/10893610/32ef92db-5cbb-426d-acf9-2327b695edf8/600x900",
    //    Description =
    //        "� ����� ������� ������� ����� ����������, ������������� ����� � ��� ���������� ������ (� ���� � ����� ����������). �������� ��������� �������� � ��������� �������� ����� ��������� ��������� ������������ � ���������� ����� �� ���������, ��� ����� ������ ����, ������� ������ ������� ���������� � ����� �� ��������� ������� � �������� �� � ������������� ����. �� ��-�� ������������� ������ ������ �� ����������� ����� ������ ������ �� ������������� ����� ���������, � ����������� ���� ����������� ���.",
    //    ReleaseFilmDate = 1968,
    //    Type = TypeFilm.Comedy,
    //    Director = "������ ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/aDvuA9xGVbE?si=NQFLkCQjj1QV_QP7"
    //};
    //film4.Links.Add(@"https://www.kinopoisk.ru/film/46225/");
    //film4.Price.Add("������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"https://my.mail.ru/v/ussr_hd/video/films/1768.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "�������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/9bb0f260-1c5f-4698-91cc-de481bfd0f55/600x900",
    //    Description =
    //    "��������������, 1960-� ����. ������� ���� ��������� �������, � �������� ������ �� ���� �������� �� ������� ���� �����������������. ������� �� ��������� �����, ��� � ����� ������������ � ������. �� ������ ��� �������� � ������� ��������� ���������� �� ����� ���������, ��� � ���������� �������� ����������� ������ ���� ��������. ���������� �� �������, ������� ���������� � ����� ����������� � �������� ����������� ��������� � �������.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Comedy,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/9F2-eR2dfMY?si=2RTrwUknUX9W3jfu"
    //};
    //film5.Links.Add(@"https://wax.www-lord.stream/317-film-krujella.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("C ��������");
    //film5.Links.Add(@"https://my.mail.ru/mail/alive-bars/video/_myvideo/412.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //Film film6 = new Film()
    //{
    //    Name = "���� � ������",
    //    PathToImage = @"https://kinoafisha.ua/upload/2021/12/films/9751/23hj4w7kdog.jpeg",
    //    Description =
    //        "���������������� ������� ������� ������ ����� ������ �������� ��������� � �����, �� ��-�� ������ ������ �������� ���������� ������. ����� ������� ���� �� ��� �����������, ������� ���� �������: � ������� ���� � ����� ��������� ��������� �� �������� � ���������� ������� ������ ������� ��������� � ������� ����������� ������� ���� � ����� �������� ������������������ �����������. ��������� ������ ������ ������, ������� ��� ������� �� ������ �� �������������� ���������, � ��� ������� ����� �� ����� ������� �������� �������.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Drama,
    //    Director = "��� �������, ������� �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/W2uQ1wJOPRk?si=-A6aZ7vgI6hyNaxd"
    //};
    //film6.Links.Add(@"https://d.lordfilm15.site/65-lulu-i-briggs.html");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("C ��������");
    //film6.Links.Add(@"https://www.kinopoisk.ru/film/1355139/");
    //film6.Price.Add("������");
    //film6.Advertisement.Add("��� �������");
    //Film film7 = new Film()
    //{
    //    Name = "��������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/aae3a928-6465-4bed-9af4-16929a44fd79/600x900",
    //    Description =
    //    "������� ��������� ��������� ����� ����� �� ������. ���������� ���� � ����� ������� �����������, �������� �������� � ������� � ������������ ���������� ������������. ������� ���� ���� ���������, ������ �� ���������� �� �������, ���������� ���� �������� ��� � ���������� ����������� � ��������������� � ������� ����� ���. �������� ������ � ������ �����������, ������ ������������ � �������� ������������ ������� � ���������� �� ������� ���� ������� �����������. ��� �� ��� � ������ �������� ��������� ���� �� ��������������� ������.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Drama,
    //    Director = "��� ����-��",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/GGnM74uxjlo?si=XpeqjTg1W_I0O6CG"
    //};
    //film7.Links.Add(@"https://my.mail.ru/mail/kremlev.vladimir/video/453/91183.html");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");
    //film7.Links.Add(@"https://www.kinopoisk.ru/film/1043758/");
    //film7.Price.Add("������");
    //film7.Advertisement.Add("��� �������");
    //Film film8 = new Film()
    //{
    //    Name = "����� � �����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/12feddd0-1f10-4608-b6c7-2d0bad2bdceb/600x900",
    //    Description =
    //        "� ��������� ��-�� ���� ��������. ������� ����� � ����� � ����� � ����� ����� ����������� � ���� � �����. �� ������ ��������� ��������������, � �� ������ ������������ ���������� �����. ���������� ������ � ����� � ������������� ������, ����������� ����� ������ � �������� �������� �� ���������. 442 ��������� � ����. ����� ���������� �� ���� � ������� � � ���� ����� ������������ ������, ����� �������� ���������",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Horrors,
    //    Director = "�� ���-��",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/58r-Rq_TuEI?si=qhx7pFpt4ImI93Cw"
    //};
    //film8.Links.Add(@"https://my.mail.ru/mail/raskovalov64/video/_myvideo/15950.html");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("��� �������");
    //film8.Links.Add(@"https://www.kinopoisk.ru/film/977288/");
    //film8.Price.Add("������");
    //film8.Advertisement.Add("��� �������");

    //Film film1 = new Film()
    //{
    //    Name = "��������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/757f5bd9-b85c-4220-9b8d-5548a3436937/600x900",
    //    Description =
    //    "1930-� ����, ����� ��� ������� ������. �������� �� �������� ���� ���������� ������ ������� ���-�� ������������� � ����, ������ �������� ���������. �� ������������ ��������� ������� ������ ������, ������� � ��� � ������, ��������, ����� �������� � �����������, ����� � ������ � ��������� ����������. � ���-�� ������ ��� ������, ��������� � ��������� � ������. ��� ��� � �������. �� �������� ��������� ���� ����� ����������� ��� �������.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Thriller,
    //    Director = "��� ����-��",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/-nF9DLj8YcQ?si=J2edVtkQpwf3W0lQ"
    //};
    //film1.Links.Add(@"https://my.mail.ru/mail/nursik_bekkozha/video/_myvideo/11.html");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //film1.Links.Add(@"https://b.lordfilms0.org/425-sluzhanka-film-2016-06");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("� ��������");
    //Film film2 = new Film()
    //{
    //    Name = "������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/f91ef1b3-aeff-4835-8117-8a23781f2533/600x900 ",
    //    Description =
    //        "1988 ���. ������������ � ����� ���������������� ��������� � ��-c� � ���� �������� ������ �� ���� ����� ����������, �������� ���������� � ����������� �������� � ����������� �������. �� ������� ��� ��� ��� �������������� �������� ���� �������. ���� ��� ������ ���� ������������� �������, � ��-�� ���������. ����������� �������� ��� � �������� � ������� ��� ���� �� 15 ���.",
    //    ReleaseFilmDate = 2003,
    //    Type = TypeFilm.Thriller,
    //    Director = "��� ����-��",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/vtaNksc6roc?si=f7_--UxGva5uvsGF"
    //};
    //film2.Links.Add(@"https://my.mail.ru/mail/grandcoosh/video/26/1632.html");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://hd1.33lordfilm-0.xyz/33944-oldboi.html");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("� ��������");
    //Film film3 = new Film()
    //{
    //    Name = "���� � ����-�����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1946459/5c758ac0-7a5c-4f00-a94f-1be680a312fb/600x900",
    //    Description =
    //    "1987 ���. ������� ������� ���������� �������� � �������� �������������� �����. ������ ���� ����������� ����� ���������� ������ ������� ���-������. �� ������ ���� ������ ������� ������������ � ��������� ���������, ������������ ������� �������. ��� ����������� ����� ������� � ��������� � ��������� ������� ������ ��� ���� �����. �� ���������� � ������� �� ���� �����, ���������, ������� ����� ������� ����� ���� � ��������� � ������ ������� � ��� ����������� �����. � �������� ����������� ��� �������� ���������� ������ ��������, ��� ���� ����� � �������� �������� ��������� �������. � ��������� �� ������ ����� ������� ��������� �����: �������� �� ����� ��������� � ������, �������� � ����������� ��������� � �������������, ����������� ��������� ������������� ����������, � ��� ����� ������ � �������. ������� ��������� ������, ����� ������� ����������� �������� �������� �������������� ����� ���...",
    //    ReleaseFilmDate = 2013,
    //    Type = TypeFilm.Drama,
    //    Director = "������ ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/CHivqmutR0I?si=H5ZIvm5RvUqH1LLe"
    //};
    //film3.Links.Add(@"https://www.kinopoisk.ru/film/462682/");
    //film3.Price.Add("������");
    //film3.Advertisement.Add("��� �������");
    //Film film4 = new Film()
    //{
    //    Name = "������� ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1777765/bb8afbd6-c9cd-4631-99e9-3fecf241dbaf/600x900",
    //    Description =
    //        "�� ��������� ���� ����� ������������ 85-����� ���������� ������ ������������ ������� ������� ������ ��������� ��������� ������� ������. ������ � ����� ������������, �� ������� �� ��������� ���������� ���� �������������� � �������� ������ �����, ����, � ���� ���� ������ ������������� ������� �������� ����� ����. ��� �� ����� �� ������� ������� � ��������� �� ������������ � ����� �� ������������� ������ �������. �� ����� ���� ������� ������������, ����� ������, ��� ��� ������������� ���� ��������� � �������� ������ ���������, �� ����� �������� ��������� ������� � ��������� ���������, ������� ��������� �� ������� ����.",
    //    ReleaseFilmDate = 2019,
    //    Type = TypeFilm.Detective,
    //    Director = "����� �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/8VMvCavnFNw?si=B3o0aGUwAa6u7Wx3"
    //};
    //film4.Links.Add(@"https://my.mail.ru/mail/vladimir.1336/video/211/22145.html");
    //film4.Price.Add("������");
    //film4.Advertisement.Add("��� �������");
    //film4.Links.Add(@"https://www.kinopoisk.ru/film/1188529/");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "������� �����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/4b27e219-a8a5-4d85-9874-57d6016e0837/600x900",
    //    Description =
    //    "1960-� ����. ����� �������� ���-��������� ������� ����� �� ������ �������� ���� �� �������� ������ ���� ���������� �� ���� �������. ��� ��� � ��� ����� ��� ����� � ���������� �������� ���, ������� � ����������� ���������� ��������, ����������� ������������ ������ � ���������� � ����� �� ����� ������, ��� ��� ������ ���������� ��������� � ����� ����������. �� �������� ���� � �������� ��������, ������������� � ��������, ���������� ������ ������� ��������. � ���� ����� ��� ���� ������, � ��� ������� �������� ������� ����� �����.",
    //    ReleaseFilmDate = 2011,
    //    Type = TypeFilm.Biography,
    //    Director = "����� ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/e6b9urtUJt0?si=YJbU1fLuxHjIdomT"
    //};
    //film5.Links.Add(@"https://my.mail.ru/mail/avan.mad/video/_myvideo/66.html");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://www.kinopoisk.ru/film/1108577/");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //Film film6 = new Film()
    //{
    //    Name = "���������� ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1898899/8ef070c9-2570-4540-9b83-d7ce759c0781/600x900",
    //    Description =
    //        "��������� ��������� �������� �������� ����������� ����������� � �������� �������� ��������� �� ���������� ������� �����. ������� � ��������� ������������ �� ��������� ������� ������� ĸ����� � ���������������� �������� ����� � ����������� ����������. ������ ������, ��� ��������������������� � ���� ������, � ������������, ���� ���� ����� ����, � ��������������. \r\n�������� ������� �������, � ��� ��� ����� ������ ����� ���� ����� ����� ��� �� ������� ����� �����, � ��������� �������� ���������� �� ������ ����������. �������� ������ ������ � ������� �������� ���������� ����������, ��� ���������� ������ ���������� ����, ������� �������� ������������ ����������� �������������.",
    //    ReleaseFilmDate = 1999,
    //    Type = TypeFilm.Thriller,
    //    Director = "����� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/C7-7qQ61QHU?si=eiIEFHCSTLTL65uq"
    //};
    //film6.Links.Add(@"https://my.mail.ru/mail/vm_gluschenko/video/110735/237319.html");
    //film6.Price.Add("���������");
    //film6.Advertisement.Add("��� �������");
    //film6.Links.Add(@"https://www.kinopoisk.ru/film/361/");
    //film6.Price.Add("������");
    //film6.Advertisement.Add("��� �������");
    //Film film7 = new Film()
    //{
    //    Name = "����������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4716873/893bba73-3105-4944-8d18-a38f929f2759/600x900",
    //    Description =
    //    "����� ���������� � ���������� � ����������� �����, ���������� ������ ������� ���������, �� �������� ������ �� ��������� �����. ���������� �������� �� ������, ��������� ����������� ������������ ����� �������� ������ ������� � ����� ���� � ������� ����� ������� ������ � ������������� ����������. � ���� ������ ���������� ����� ������ �������, �������������� ������� ����� �����, ������� � ������ ���� ������ ��������, ��� ������ ���� ��������� � �������� ����� ������� � ������� �����������. ����� ��������� �� ������ �� ����������� �������� ����, �������� �� ��, ��� �� ��������� ����� ��������� ������ ��� ��� �����. ������ �� ��������� �������� ������� ����, �� �������� ����� �������� ������ ���� ���������� �����������.",
    //    ReleaseFilmDate = 2016,
    //    Type = TypeFilm.Cartoon,
    //    Director = "������ ������, ��� ���, ������ ���",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/N6zm52tRF0c?si=WsXBDfCCi7IdbqDs"
    //};
    //film7.Links.Add(@"https://wax.www-lord.stream/308-multfilm-zveropolis.html ");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");
    //film7.Links.Add(@"https://vk.com/video-188008198_456239133");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("��� �������");
    //Film film8 = new Film()
    //{
    //    Name = "������ ���������",
    //    PathToImage = @"https://b1.filmpro.ru/c/102900.jpg",
    //    Description =
    //        "��� ������������ �������� �������� ������������ �� ���� �� �������� � ����� �����������, ����� ������������ ������������ ��������� ������� ��� ����������� ������������. ��� ���������� ������������� �� �������� ����������� � �������� ���, ������������ �������� � ����������� ������ ���������� �������.",
    //    ReleaseFilmDate = 2009,
    //    Type = TypeFilm.Thriller,
    //    Director = "������ ��������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/_l7R9Rz5URw?si=fy096UnsB6qGeqYi"
    //};
    //film8.Links.Add(@"https://my.mail.ru/mail/vm_gluschenko/video/56039/243400.html");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("��� �������");
    //film8.Links.Add(@"https://www.kinopoisk.ru/film/397667/");
    //film8.Price.Add("������");
    //film8.Advertisement.Add("��� �������");



    //Film film1 = new Film()
    //{
    //    Name = "������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1629390/8ab9a119-dd74-44f0-baec-0629797483d7/600x900",
    //    Description =
    //    "���� � ����������� ���, ������ �� ������ � ������� ��������� ����������: �� ������ ������ ������� �� ������ ����������� �� ����� ���, ����� ������������ ����� �������� ������. ������ ����������� ����� ������� ��� ������ ������� � ��������� � ������������� ���� ������������� ��������, �� ��� �� ���������� ��� � ��������� ������� � ������ �����, ��� �� �����-���� �����.",
    //    ReleaseFilmDate = 2010,
    //    Type = TypeFilm.Fantasy,
    //    Director = "��������� �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/85Zz1CCXyDI?si=eHJHC_sqiClycuSV"
    //};
    //film1.Links.Add(@"https://nachalo-lordfilm.ru/");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("� ��������");
    //film1.Links.Add(@"https://my.mail.ru/list/svarog.1975/video/670/69216.html ");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("��� �������");
    //Film film2 = new Film()
    //{
    //    Name = "�������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/96d93e3a-fdbf-4b6f-b02d-2fc9c2648a18/600x900",
    //    Description =
    //        "� ������ � ��������� �������� ��������� ��������� ����������� ����. �������� ������ ������ ���� ������� ����� � �����, � ������� ���������� ���� ������������ � �������, ����� ����� ����� �� �������. ������� ������� ����� ������ �������� ���������, � ���� �� ��������� �������� �������� ��������� ���������, � �������, �������� �� ���� ������������ ������������� �������.",
    //    ReleaseFilmDate = 1997,
    //    Type = TypeFilm.Melodrama,
    //    Director = "������ �������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/Qwx0AsI6Zq0?si=ke5sMGLpYo1QMHdm"
    //};
    //film2.Links.Add(@"https://my.mail.ru/bk/wowa5997/video/_myvideo/332.html");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://wax.www-lord.stream/318-film-titanik.html");
    //film2.Price.Add("��������");
    //film2.Advertisement.Add("� ��������");
    //Film film3 = new Film()
    //{
    //    Name = "���� � ��������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/1073b002-96de-49ae-a432-1da460f6890c/600x900",
    //    Description =
    //    "��� ��-��� ��� �������, �������, �� ��� ������ � ������ � ����� �� ��� � ���������� ������. ���� ���������� �� ������� ������ � ��� ����� ����� �� �������������, � ������� �� ��������� � ����� ����������� ����������, ������� ������� ���������� ������� � ������� ����, � ����� ������� ��-���� ������� ����� � �������. �� ������� ������� �������� ���������, ����� �� �����, ��� ������ ���� � ����� ����� ���������� ������ ��� ���� � �������. �� ������ �� ������ � ������� � ���������� ��������� ���������� ������ ��� �� ��������� � ������ � 40 ��������� ��������. ����� ��������� �� ��������� ����������� ��� ���� ������� � ����������� ���������, ������, ���������� ������� ������, ��������������� �����������, ��������� �� ��������� � ������ ������ �������� ����������� � �������.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Thriller,
    //    Director = "���� ���-���",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/ef0IPK9lljw?si=TnrxVXTsRmr9mZcw"
    //};
    //film3.Links.Add(@"https://my.mail.ru/mail/oks.petra/video/Igra_v_kalmara/1081.html");
    //film3.Price.Add("��������");
    //film3.Advertisement.Add("��� �������");
    //film3.Links.Add(@"https://20lordserial.xyz/429-igra-v-kalmara-sv-29.html");
    //film3.Price.Add("��������");
    //film3.Advertisement.Add("� ��������");
    //Film film4 = new Film()
    //{
    //    Name = "�������� �������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/6201401/22f75234-c22e-43df-8e03-11a7eff64a4e/600x900",
    //    Description =
    //        "��������������� ��� ���-�� � ���������� �� ������ ����� ��������� �������� ��������� � �������� � ���� ���� ������������ ����������. ��� �������� ����������� � ����� ������ ������� �������.",
    //    ReleaseFilmDate = 2020,
    //    Type = TypeFilm.Melodrama,
    //    Director = "��� ���-���",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/BhP1eYQ5Pxk?si=3qRZ1HOLgMVBhPAP"
    //};
    //film4.Links.Add(@"https://tv.lordserialu3.online/5253-istinnaja-krasota2f5.html");
    //film4.Price.Add("���������");
    //film4.Advertisement.Add("� ��������");
    //film4.Links.Add(@"https://www.kinopoisk.ru/series/1398965/");
    //film4.Price.Add("������");
    //film4.Advertisement.Add("��� �������");


    //Film film5 = new Film()
    //{
    //    Name = "�������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4774061/9898b051-a211-47ce-846d-728ca83c1c00/600x900",
    //    Description =
    //    "��� ��������� ��� ������������ ���� � �������� �������� COVID-19, ����� �������� ��������� �������� ���������� ����������� �����, ������������ � ������������� ������� � ��������� �� �����. � ����� �� ���������� ������������ �� ��-��� � ������ ������� �� ������ �� ������ � ����������� � �, ������� �� ���� ��������, ������������ � ��������� ����� �� ������ � ����� �������. ������������ �� �������� � �� ���������, � ��-��� ��������� ������ �� ����������� �������� � �������� ��������� ���� � ������ ������, �������������� ����������� ��� �-�����, �������� �������� � ����� ����.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Fantasy,
    //    Director = "�� ����-��",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/IwWDhzw5CtQ?si=e8Akwi7VdFBVtn5k"
    //};
    //film5.Links.Add(@"https://m.my.mail.ru/my/welcome?back_after_reg=https://m.my.mail.ru/bk/anveela/video/937");
    //film5.Price.Add("���������");
    //film5.Advertisement.Add("��� �������");
    //film5.Links.Add(@"https://www.kinopoisk.ru/series/4402867/");
    //film5.Price.Add("������");
    //film5.Advertisement.Add("��� �������");
    //Film film6 = new Film()
    //{
    //    Name = "������ ����� �� 80 ����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/cefc0812-8400-4e1e-9a04-3bdd0f74981a/600x900",
    //    Description =
    //        "��������� ����������, ���������������� �������� �� ����, ������� �� 80 ���� ����������� ������������ �����������. ��������������� ��������� � ����������� ����������� ������������ � ��� �� ��������.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Comedy,
    //    Director = "���� ������, ������ �����, ������ �����",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/RUpfhB-7cBU?si=GMiDmREgVd2xeVuR"
    //};
    //film6.Links.Add(@"https://premier.one/show/vokrug-sveta-za-80-dnej");
    //film6.Price.Add("������");
    //film6.Advertisement.Add("��� �������");
    //film6.Links.Add(@"https://www.kinopoisk.ru/series/1337642/");
    //film6.Price.Add("������");
    //film6.Advertisement.Add("��� �������");
    //Film film7 = new Film()
    //{
    //    Name = "��� �������",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/4303601/4939065f-efa2-4192-a3c2-cec534e79e01/600x900",
    //    Description =
    //    "���������� ��������� ���� � ������ � ������ ��������. ��������� ���� � �������� ����� �� �������� ����.",
    //    ReleaseFilmDate = 2022,
    //    Type = TypeFilm.Fantasy,
    //    Director = "���� ������, ���� ������ �����, ������ ��������",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/5weHputuO4g?si=uc4Alqasy2fTVmfW"
    //};
    //film7.Links.Add(@"https://tv3.lordfilms4.online/715-dom-drakona-47e.html");
    //film7.Price.Add("���������");
    //film7.Advertisement.Add("� ��������");
    //film7.Links.Add(@"https://www.kinopoisk.ru/series/1316601/ ");
    //film7.Price.Add("������");
    //film7.Advertisement.Add("��� �������");
    //Film film8 = new Film()
    //{
    //    Name = "����",
    //    PathToImage = @"https://avatars.mds.yandex.net/get-kinopoisk-image/10809116/70aac55f-cd33-4e69-94c1-efd47405805a/600x900",
    //    Description =
    //        "�������� ������ �� �������� �������� �� �������� ������ � ������ ��� ������. ������ � ������������ ������ ������� �������� ������ ������, ��� ������������� ������� ��������� ������������ � ������������ �����, ����������� � ��������� �� 99% ���������� ��� ���������� � ��������. ������ 10 ���, �� ����� ������ ������� �� ����� ��������� ��� ����� � ��� ����� ��������� ������ �������� ���������, ����������� �������-���������, �������� ������������ � ���������� ���������������. �� ��� �� ���� ��� �� ������������.",
    //    ReleaseFilmDate = 2021,
    //    Type = TypeFilm.Thriller,
    //    Director = "��� ����-�, ���� ����-��",
    //    FilmOrSerial = FilmOrSerial.Serial,
    //    Preview = @"https://www.youtube.com/embed/S6CAz71l-IM?si=1PTWLfYIRST6PdFS"
    //};
    //film8.Links.Add(@"https://m.my.mail.ru/my/welcome?back_after_reg=https://m.my.mail.ru/bk/anveela/video/716");
    //film8.Price.Add("���������");
    //film8.Advertisement.Add("��� �������");
    //film8.Links.Add(@"https://www.kinopoisk.ru/series/1387128/");
    //film8.Price.Add("������");
    //film8.Advertisement.Add("��� �������");
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
