using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.DAL.Repositories
{
    public class SiteFeedbackRepository : ISiteFeedbackRepository
    {
        private readonly ApplicationDbContext _db;

        public SiteFeedbackRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(SiteFeedback entity)
        {
            await _db.SiteFeedback.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<bool> Delete(SiteFeedback entity)
        {
            throw new NotImplementedException();
        }

        public Task<SiteFeedback> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SiteFeedback>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SiteFeedback entity)
        {
            throw new NotImplementedException();
        }
    }
}