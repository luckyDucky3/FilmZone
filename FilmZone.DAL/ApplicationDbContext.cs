﻿using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Models;
using Microsoft.EntityFrameworkCore;
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
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=filmdb;Username=postgres;Password=100506Ki");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подключения к БД: {ex}");
            }
        }
        public DbSet<Film> Film { get; set; }
        public DbSet<User> User { get; set; }
    }

}