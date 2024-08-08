using FilmZone.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmZone.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmZone.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(User entity)
        {
            await _db.User.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetById(int id)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> Select()
        {
            return await _db.User.ToListAsync();
        }

        public async Task<bool> Delete(User entity)
        {
            _db.User.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(User entity)
        {
            _db.User.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetByLogin(string name)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Login == name);
        }

        public async Task<User> GetByMail(string mail)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Email == mail);
        }
    }
}
