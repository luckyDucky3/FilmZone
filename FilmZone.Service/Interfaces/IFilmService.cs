using FilmZone.Domain.ViewModels;
using FilmZone.Domain.Response;
using FilmZone.Domain.Models;

namespace FilmZone.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IBaseResponse<IEnumerable<Film>>> GetFilms();

        Task<IBaseResponse<FilmViewModel>> GetFilm(int id);

        Task<IBaseResponse<FilmViewModel>> CreateFilm(FilmViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteFilm(int id);

        Task<IBaseResponse<Film>> GetFilmByName(string name);

        Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model);
    }
}
