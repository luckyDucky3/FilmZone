using FilmZone.Domain.Models;
using FilmZone.Domain.Response;

namespace FilmZone.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<IEnumerable<User>>> GetUsers();

        Task<IBaseResponse<User>> GetUserById(int id);

        Task<IBaseResponse<bool>> CreateUser(User user);

        Task<IBaseResponse<bool>> DeleteUser(int id);

        Task<IBaseResponse<User>> GetUserByLogin(string name);
        Task<IBaseResponse<User>> GetUserByMail(string mail);

        Task<IBaseResponse<User>> UpdateUserById(User _user);
    }
}
