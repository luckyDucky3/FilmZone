using FilmZone.Domain.Models;
using FilmZone.Domain.Response;

namespace FilmZone.Service.Interfaces
{
    public interface IBestFilmService
    {
        Task<IBaseResponse<bool>> CreateFilm(BestFilm bestFilm);
        Task<IBaseResponse<List<BestFilm>>> GetListOfUserFilm(string login);
        public Task<IBaseResponse<bool>> DeleteFilmByUser(string login, string filmName);
    }
}