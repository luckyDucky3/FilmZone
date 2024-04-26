using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using FilmZone.Service.Interfaces;

namespace FilmZone.Service.Implementations
{
    public class UserService : IUserService
    {
        public Task<IBaseResponse<IEnumerable<User>>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<User>> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<User>> CreateUser(FilmViewModel carViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<Film>> GetUserByLastName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseResponse<Film>> Edit(int id, User model)
        {
            throw new NotImplementedException();
        }
    }
}
