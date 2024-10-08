﻿using FilmZone.Service.Interfaces;
using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Models;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace FilmZone.Service.Implementations
{

    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<IBaseResponse<FilmViewModel>> GetFilmById(int id)
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
                    Description = $"[GetFilmById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateFilm(Film film)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _filmRepository.Create(film);
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
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
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
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

        public async Task<IBaseResponse<bool>> UpdateFilm(string name, Film model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var film = await _filmRepository.GetByName(name);
                if (film == null)
                {
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Description = "Film not found";
                    baseResponse.Data = false;
                    return baseResponse;
                }
                film.PathToImage = model.PathToImage;
                film.Description = model.Description;
                film.Preview = model.Preview;
                film.Type = model.Type;
                film.FilmOrSerial = model.FilmOrSerial;
                film.Director = model.Director;

                film.Links = model.Links;
                film.Advertisement = model.Advertisement;
                film.Price = model.Price;
                
                //film = TransformToFilm(ref model);
                baseResponse.Data = await _filmRepository.Update(film);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[EditFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
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
                    baseResponse.Description = "Film not found";
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
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

        public async Task<IBaseResponse<List<Film>>> GetFilmByType(TypeFilm type)
        {
            var baseResponse = new BaseResponse<List<Film>>();
            try
            {
                var film = await _filmRepository.GetByType(type);
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
                return new BaseResponse<List<Film>>()
                {
                    Description = $"[GetFilmByType] : {ex.Message}",
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
                Advertisement = new List<string>(film.Advertisement),
                Rating = film.Rating
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
                Advertisement = new List<string>(film.Advertisement),
                Rating = film.Rating
            };
            return model;
        }

        public async Task<IBaseResponse<bool>> UpdateFilmRating(int filmId, double rating)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var film = await _filmRepository.GetById(filmId);
                if (film == null)
                {
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Description = "Film not found";
                    baseResponse.Data = false;
                    return baseResponse;
                }
                film.Rating = rating;
                baseResponse.Data = await _filmRepository.Update(film);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[EditFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public async Task<IBaseResponse<List<Film>>> GetFilmsInOrderByRating(int countFilms)
        {
            var baseResponse = new BaseResponse<List<Film>>();
            try
            {
                var film = await _filmRepository.GetFilmOrSerialsByRating(countFilms);
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
                return new BaseResponse<List<Film>>()
                {
                    Description = $"[GetFilmsInOrderByRating] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<Film>>> GetFilmsByReleaseDate(int countFilms)
        {
            var baseResponse = new BaseResponse<List<Film>>();
            try
            {
                var film = await _filmRepository.GetFilmsByReleaseDate(countFilms);
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
                return new BaseResponse<List<Film>>()
                {
                    Description = $"[GetFilmsByReleaseDate] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<Film>>> GetSerialsByReleaseDate(int countFilms)
        {
            var baseResponse = new BaseResponse<List<Film>>();
            try
            {
                var film = await _filmRepository.GetSerialsByReleaseDate(countFilms);
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
                return new BaseResponse<List<Film>>()
                {
                    Description = $"[GetSerialsByReleaseDate] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
