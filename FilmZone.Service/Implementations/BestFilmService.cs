using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Service.Implementations
{
    public class BestFilmService : IBestFilmService
    {
        private readonly IBestFilmsRepository bestFilmsRepository;
        public BestFilmService(IBestFilmsRepository bestFilmsRepository)
        {
            this.bestFilmsRepository = bestFilmsRepository;
        }

        public async Task<IBaseResponse<bool>> CreateFilm(BestFilm bestFilm)
        {
            var response = new BaseResponse<bool>();
            try
            {
                await bestFilmsRepository.Create(bestFilm);
                response.StatusCode = Domain.Enum.StatusCode.OK;
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
            return response;
        }

        public async Task<IBaseResponse<List<BestFilm>>> GetListOfUserFilm(string login)
        {
            var response = new BaseResponse<List<BestFilm>>();
            try
            {
                response.Data = await bestFilmsRepository.GetAllUserFilms(login);
                response.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<BestFilm>>()
                {
                    Description = $"[GetListOfUserFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return response;
        }
        public async Task<IBaseResponse<bool>> DeleteFilmByUser(string login, string filmName)
        {
            var response = new BaseResponse<bool>();
            BestFilm bestFilm = new BestFilm();
            bestFilm.FilmName = filmName;
            bestFilm.UserName = login;
            try
            {
                response.Data = await bestFilmsRepository.Delete(bestFilm);
                response.StatusCode=StatusCode.OK;
                return response;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilmByUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}