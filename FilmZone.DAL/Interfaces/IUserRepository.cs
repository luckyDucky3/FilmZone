using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FilmZone.Domain.Models;

namespace FilmZone.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByLogin(string name);
        Task<User> GetByMail(string name);
    }
}