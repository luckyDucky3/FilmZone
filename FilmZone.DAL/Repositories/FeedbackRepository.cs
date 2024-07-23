using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.DAL.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedbackRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Feedback entity)
        {
            await _db.Feedback.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(Feedback entity)
        {
            throw new NotImplementedException();
        }

        public Task<Feedback> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Feedback>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<Feedback> Update(Feedback entity)
        {
            throw new NotImplementedException();
        }
    }
}