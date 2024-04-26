using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();

        Task<IBaseResponse<User>> GetUser(int id);

        Task<IBaseResponse<User>> CreateUser(FilmViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<Film>> GetUserByLastName(string name);

        Task<IBaseResponse<Film>> Edit(int id, User model);
    }
}
