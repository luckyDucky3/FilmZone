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

        Task<IBaseResponse<bool>> UpdateFilm(string name, Film model);
        Task<IBaseResponse<List<Film>>> GetFilmByType(TypeFilm type);
        Task<IBaseResponse<bool>> UpdateFilmRating(int filmId, double rating);
        Task<IBaseResponse<List<Film>>> GetFilmsInOrderByRating(int countFilms);
        Task<IBaseResponse<List<Film>>> GetFilmsByReleaseDate(int countFilms);
        Task<IBaseResponse<List<Film>>> GetSerialsByReleaseDate(int countFilms);
    }
}
