using FilmZone.Domain.ViewModels;
using FilmZone.Domain.Response;
using FilmZone.Domain.Models;
using FilmZone.Domain.Enum;

namespace FilmZone.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IBaseResponse<IEnumerable<Film>>> GetFilms();

        Task<IBaseResponse<FilmViewModel>> GetFilmById(int id);

        Task<IBaseResponse<bool>> CreateFilm(Film film);

        Task<IBaseResponse<bool>> DeleteFilm(int id);

        Task<IBaseResponse<Film>> GetFilmByName(string name);

        Task<IBaseResponse<Film>> UpdateFilm(string name, Film model);
        Task<IBaseResponse<List<Film>>> GetFilmByType(TypeFilm type);
    }
}
