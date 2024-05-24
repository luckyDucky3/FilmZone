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

using (ApplicationDbContext db = new ApplicationDbContext()) //���������� ������
{
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
