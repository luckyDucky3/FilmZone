using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.DAL.Repositories
{
    public class FilmFeedbackRepository : IFilmFeedbackRepository
    {
        private readonly ApplicationDbContext _db;

        public FilmFeedbackRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(FilmFeedback entity)
        {
            await _db.FilmFeedback.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<FilmFeedback>> GetFeedbacks(int filmId)
        {
            return await _db.FilmFeedback.Where(x => x.FilmId == filmId).ToListAsync();
        }
        public async Task<bool> Delete(FilmFeedback entity)
        {
            if (entity.Id == 0)
            {
                entity = await _db.FilmFeedback.Where(x => x.Name == entity.Name && x.Heading == entity.Heading && x.Value == entity.Value).FirstAsync();
            }
            _db.FilmFeedback.Remove(entity);
            return true;
        }

        public Task<FilmFeedback> GetById(int id)
        {
            return _db.FilmFeedback.Where(x => x.Id ==  id).FirstAsync();
        }

        public async Task<List<FilmFeedback>> Select()
        {
            return await _db.FilmFeedback.ToListAsync();
        }

        public async Task<bool> Update(FilmFeedback entity)
        {
            _db.FilmFeedback.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<FilmFeedback> GetFeedbackByLoginAndFilmName(string login, int filmId)
        {
            return await _db.FilmFeedback.Where(x => x.Name == login && x.FilmId == filmId).FirstAsync();
        }
        public async Task<bool> UpdateFeedback(string login, int filmId, int rating)
        {
            var filmFeedback = await GetFeedbackByLoginAndFilmName(login, filmId);
            if (filmFeedback == null)
            {
                return false;
            }
            filmFeedback.Value = rating;
            _db.FilmFeedback.Update(filmFeedback);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<List<float>> GetListOfValues(int filmId)
        {
            return await _db.FilmFeedback.Where(x => x.FilmId == filmId).Select(x => x.Value).ToListAsync();
        }

        public async Task<bool> UpdateEmptyFeedbackWithRating(string login, int filmId, FilmFeedback feedback)
        {
            var filmFeedback = await GetFeedbackByLoginAndFilmName(login, filmId);
            if (filmFeedback == null)
            {
                return false;
            }
            feedback.Value = filmFeedback.Value;
            _db.FilmFeedback.Update(feedback);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
