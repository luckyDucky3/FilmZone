using FilmZone.Service.Interfaces;
using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var film = await _filmRepository.GetById(id);
                if (film == null)
                {
                    baseResponse.Description = "Film not found";
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    return baseResponse;
                }

                var data = TransformToFilmViewModel(ref film);

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
                Film film = TransformToFilm(ref filmViewModel);
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
                var film = await _filmRepository.GetById(id);
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
                var film = await _filmRepository.GetById(id);
                if (film == null)
                {
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Description = "Film not found";
                    return baseResponse;
                }

                film = TransformToFilm(ref model);
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

        private FilmViewModel TransformToFilmViewModel(ref readonly Film film)
        {
            FilmViewModel model = new FilmViewModel()
            {
                Id = film.Id,
                Name = film.Name,
                PathToImage = film.PathToImage,
                FilmOrSerial = film.FilmOrSerial,
                Description = film.Description,
                ReleaseFilmDate = film.ReleaseFilmDate,
                Type = film.Type,
                Director = film.Director,
                Preview = film.Preview,
                Links = new List<string>(film.Links),
                Price = new List<string>(film.Price),
                Advertisement = new List<string>(film.Advertisement)
            };
            return model;
        }
        private Film TransformToFilm(ref readonly FilmViewModel film)
        {
            Film model = new Film()
            {
                Id = film.Id,
                Name = film.Name,
                PathToImage = film.PathToImage,
                FilmOrSerial = film.FilmOrSerial,
                Description = film.Description,
                ReleaseFilmDate = film.ReleaseFilmDate,
                Type = film.Type,
                Director = film.Director,
                Preview = film.Preview,
                Links = new List<string>(film.Links),
                Price = new List<string>(film.Price),
                Advertisement = new List<string>(film.Advertisement)
            };
            return model;
        }
    }
}
