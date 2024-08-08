using FilmZone.DAL.Interfaces;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.Service.Implementations
{
    public class FilmFeedbackService : IFilmFeedbackService
    {
        private readonly IFilmFeedbackRepository _feedbackRepository;

        public FilmFeedbackService(IFilmFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IBaseResponse<bool>> CreateFeedback(FilmFeedback feedback)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await _feedbackRepository.Create(feedback);
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateFeedback] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<List<FilmFeedback>>> GetListOfFeedback(int filmId)
        {
            var baseResponse = new BaseResponse<List<FilmFeedback>>();
            try
            {
                baseResponse.Data = await _feedbackRepository.GetFeedbacks(filmId);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<FilmFeedback>>()
                {
                    Description = $"[GetListOfFeedback] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }
        public async Task<IBaseResponse<FilmFeedback>> GetFeedbackByLoginAndFilmName(string login, int filmId)
        {
            var baseResponse = new BaseResponse<FilmFeedback>();
            try
            {
                baseResponse.Data = await _feedbackRepository.GetFeedbackByLoginAndFilmName(login, filmId);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmFeedback>()
                {
                    Description = $"[GetFeedbackByLoginAndFilmName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> UpdateFeedbackWithoutRating(string login, int filmId, int rating)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                baseResponse.Data = await _feedbackRepository.UpdateFeedback(login, filmId, rating);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateFeedbackRating] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
            return baseResponse;
        }
        public async Task<IBaseResponse<List<float>>> GetListOfValues(int filmId)
        {
            var baseResponse = new BaseResponse<List<float>>();
            try
            {
                baseResponse.Data = await _feedbackRepository.GetListOfValues(filmId);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<float>>()
                {
                    Description = $"[GetListOfValues] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = null
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> UpdateEmptyFeedbackWithRating(string login, int filmId, FilmFeedback feedback)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                baseResponse.Data = await _feedbackRepository.UpdateEmptyFeedbackWithRating(login, filmId, feedback);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateEmptyFeedbackWithRating] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                    Data = false
                };
            }
            return baseResponse;
        }
    }
}
