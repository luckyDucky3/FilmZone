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

using (ApplicationDbContext db = new ApplicationDbContext()) //���������� ������
{
    //Film film1 = new Film()
    //{
    //    Name = "�������� �����",
    //    PathToImage = @"~/img/cloud.png",
    //    Description =
    //        "������� ������������ ��������� ������ �� ������ ����. �������������� ���������� � ����� ������� � ����� ����� � ���, " +
    //        "��� ���-�� ������������� �� ������ �������, �������� � ���� ����������� �������� � ���������� ����������� ��� �������, " +
    //        "��������� ���� ����� � ����� ������.",
    //    ReleaseFilmDate = 2012,
    //    Type = TypeFilm.Fantasy,
    //    Director = "���� � ����� �������� � ��� ������",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/K2VtiZSvwuo?si=XUF0ThhXcKrr9Gr3"
    //};
    //film1.Links.Add(@"https://vk.com/video-110645251_456240683");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("C ��������");
    //film1.Links.Add(@"https://rutube.ru/video/71fa0ae2c405383724e143f6fee38330/?t=1");
    //film1.Price.Add("���������");
    //film1.Advertisement.Add("C ��������");
    //Film film2 = new Film()
    //{
    //    Name = "������������",
    //    PathToImage = @"~/img/inter.png",
    //    Description = "����� ������, ������� ���� � ��������� �������� �������� ������������ � ������������������ �������,��������� ��������������" +
    //                      "� ������ ������������ ������ ����������� (������� ���������������� ���������������� ������������-������� ����� �������" +
    //                      "����������) � �����������, ����� ��������� ������� ����������� ��� ����������� ����������� �������� � ����� ������� �" +
    //                      "����������� ��� ������������ ���������.",
    //    ReleaseFilmDate = 2014,
    //    Type = TypeFilm.Fantasy,
    //    Director = "��������� �����",
    //    FilmOrSerial = FilmOrSerial.Film,
    //    Preview = @"https://www.youtube.com/embed/qcPfI0y7wRU?si=wip5_nml6yQBHsJO",
    //};
    //film2.Links.Add(@"https://vk.com/video-213377389_456241538");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("C ��������");
    //film2.Links.Add(@"https://my.mail.ru/mail/ppalekss/video/5/160.html");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("��� �������");
    //film2.Links.Add(@"https://lordfilmi.org/982-film-interstellar.html");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("C ��������");
    //film2.Links.Add(@"https://interstellar-lordfilms.ru/");
    //film2.Price.Add("���������");
    //film2.Advertisement.Add("C ��������");
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
