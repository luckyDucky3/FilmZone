using FilmZone.Service.Interfaces;
using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;

namespace FilmZone.Service.Implementations
{

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<IBaseResponse<FilmViewModel>> GetFilm(int id)
        {
            var baseResponse = new BaseResponse<FilmViewModel>();
            try
            {
                var film = await _filmRepository.Get(id);
                if (film == null)
                {
                    baseResponse.Description = "Film not found";
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    return baseResponse;
                }

                var data = new FilmViewModel()
                {
                    Id = film.Id,
                    Name = film.Name,
                    Description = film.Description,
                    ReleaseFilmDate = film.ReleaseFilmDate,
                    Type = film.Type,
                    Director = film.Director,
                    Preview = film.Preview,
                    LinkF = film.LinkF,
                    LinkS = film.LinkS
                };

                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = data;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmViewModel>()
                {
                    Description = $"[GetFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<FilmViewModel>> CreateFilm(FilmViewModel filmViewModel)
        {
            var baseResponse = new BaseResponse<FilmViewModel>();
            try
            {
                var film = new Film()
                {
                    Description = filmViewModel.Description,
                    Id = filmViewModel.Id,
                    Name = filmViewModel.Name,
                    ReleaseFilmDate = filmViewModel.ReleaseFilmDate,
                    Type = (TypeFilm)Convert.ToInt32(filmViewModel.Type),
                    Director = filmViewModel.Director,
                    Preview = filmViewModel.Preview,
                    LinkF = filmViewModel.LinkF,
                    LinkS = filmViewModel.LinkS
                };

                await _filmRepository.Create(film);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmViewModel>()
                {
                    Description = $"[CreateFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteFilm(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var film = await _filmRepository.Get(id);
                if (film == null)
                {
                    baseResponse.Description = "Film not found";
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Data = false;

                    return baseResponse;
                }

                await _filmRepository.Delete(film);
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Film>> GetFilmByName(string name)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await _filmRepository.GetByName(name);
                if (film == null)
                {
                    baseResponse.Description = "Film not found";
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    return baseResponse;
                }

                baseResponse.Data = film;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[GetFilmByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await _filmRepository.Get(id);
                if (film == null)
                {
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Description = "Film not found";
                    return baseResponse;
                }

                film.Id = model.Id;
                film.Name = model.Name;
                film.Description = model.Description;
                film.ReleaseFilmDate = model.ReleaseFilmDate;
                film.Type = model.Type;
                film.Director = model.Director;
                film.Preview = model.Preview;
                film.LinkF = model.LinkF;
                film.LinkS = model.LinkS;
                await _filmRepository.Update(film);

                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[EditFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Film>>> GetFilms()
        {
            var baseResponse = new BaseResponse<IEnumerable<Film>>();
            try
            {
                var films = await _filmRepository.Select();
                if (films.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = films;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Film>>()
                {
                    Description = $"[GetFilms] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
