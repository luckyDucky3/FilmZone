﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.DAL;
using Microsoft.EntityFrameworkCore;

namespace FilmZone.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _db;

        public FilmRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Film entity)
        {
            await _db.Film.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Film entity)
        {
            _db.Film.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Film> GetById(int id)
        {
            return await _db.Film.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Film> GetByName(string name)
        {
            var lowerCaseName = name.ToLower();
            if (name.Length > 2)
            {
                return await _db.Film
                    .Where(x => x.Name.ToLower().Contains(lowerCaseName))
                    .OrderBy(x => x.Name.Length)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Film>> Select()
        {
            return await _db.Film.ToListAsync();
        }

        public async Task<Film>Update(Film entity)
        {
            _db.Film.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
