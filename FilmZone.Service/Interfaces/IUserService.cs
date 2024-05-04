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

        Task<IBaseResponse<User>> GetUserById(int id);

        Task<IBaseResponse<bool>> CreateUser(User user);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<User>> GetUserByNickName(string name);

        Task<IBaseResponse<User>> Edit(int id, User model);
    }
}
