using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.DAL.Repositories
{
    public class BestFilmsRepository : IBestFilmsRepository
    {
        private readonly ApplicationDbContext _db;

        public BestFilmsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BestFilm entity)
        {
            await _db.BestFilm.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<BestFilm>> GetAllUserFilms(string login)
        {
            List<BestFilm> list = new List<BestFilm>();
            list = await _db.BestFilm.Where(x => x.UserName == login).ToListAsync();
            await _db.SaveChangesAsync();
            return list;
        }

        public async Task<bool> Delete(BestFilm entity)
        {
            if (entity.Id == 0)
            {
                entity = await _db.BestFilm.Where(x => x.UserName == entity.UserName && x.FilmName == entity.FilmName).FirstOrDefaultAsync();
            }
            _db.BestFilm.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<BestFilm> GetById(int id)
        {
            return await _db.BestFilm.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<BestFilm>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(BestFilm entity)
        {
            throw new NotImplementedException();
        }
    }
}