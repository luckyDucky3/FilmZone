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

        public async Task<User> Get(int id)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.ID == id);
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

        public async Task<User> Update(User entity)
        {
            _db.User.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetByNickname(string name)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.NickName == name);
        }
    }
}
