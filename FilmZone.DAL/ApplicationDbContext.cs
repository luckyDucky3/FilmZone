﻿using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace FilmZone.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=filmdb;Username=postgres;Password=100506Ki").IsConfigured)
                    Console.WriteLine("Подключение к бд прошло успешно");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения к БД: {ex}");
            }
        }
        public DbSet<Film> Film { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SiteFeedback> SiteFeedback { get; set; }
        public DbSet<FilmFeedback> FilmFeedback { get; set; }
        public DbSet<BestFilm> BestFilm { get; set; }
        static void Main() 
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Console.ReadLine();
        }
    }

}