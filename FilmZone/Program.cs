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

//using (ApplicationDbContext db = new ApplicationDbContext()) //���������� ������
//{
//    Film film1 = new Film()
//    {
//        Name = "�������� ����",
//        Description = "� �������� ������� ����� ������� ���������� ��� ����� ��������������� ����� ������������ �����������." +
//                      "����� ��������, ����� ������������ �� ������� ��������, ���������� �������� ������������ �����. �������" +
//                      "�� ����� ���������� ������ ���� �����, � ������ ��� �� ������ ������ ������ ����� � ��� �����.",
//        ReleaseFilmDate = 1979,
//        Type = TypeFilm.Military,
//        Director = "������ ������",
//        Preview = "https://www.youtube.com/embed/-3ZoAp6owdk",
//        LinkF = "https://vk.com/video-176294899_456240989",
//        LinkS = "https://yandex.ru/video/preview/1212790627695649234"
//    };
//    Film film2 = new Film()
//    {
//        Name = "������������",
//        Description = "����� ������, ������� ���� � ��������� �������� �������� ������������ � ������������������ �������,��������� ��������������" +
//                      "� ������ ������������ ������ ����������� (������� ���������������� ���������������� ������������-������� ����� �������" +
//                      "����������) � �����������, ����� ��������� ������� ����������� ��� ����������� ����������� �������� � ����� ������� �" +
//                      "����������� ��� ������������ ���������.",
//        ReleaseFilmDate = 1979,
//        Type = TypeFilm.Fantasy,
//        Director = "��������� �����",
//        Preview = "https://www.youtube.com/embed/qcPfI0y7wRU",
//        LinkF = "https://interstellar-lordfilm.ru/",
//        LinkS = "https://kion.ru/video/movie/490694316"
//    };
//    Film film3 = new Film()
//    {
//        Name = "�������: ���������",
//        Description = "���� ����� ����� �������� ���������� ����, ��� ������ �� ��������� ��� ������ �������",
//        ReleaseFilmDate = 2003,
//        Type = TypeFilm.Fantasy,
//        Director = "Ѹ���� ��������",
//        Preview = "https://www.youtube.com/embed/KjKQGoZDiq4",
//        LinkF = "https://vk.com/video-127060916_456239110",
//        LinkS = "https://rutube.ru/video/c68d9ffbe75b9ca5c0f2e5d75e9ae448/?t=5"
//    };
//    db.Film.AddRange(film1, film2, film3);
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
