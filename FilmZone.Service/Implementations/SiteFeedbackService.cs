using FilmZone.DAL.Interfaces;
using FilmZone.DAL.Repositories;
using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Domain.ViewModels;
using FilmZone.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.Service.Implementations
{
    public class SiteFeedbackService : ISiteFeedbackService
    {
        private readonly ISiteFeedbackRepository _feedbackRepository;

        public SiteFeedbackService(ISiteFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IBaseResponse<bool>> CreateFeedback(SiteFeedback feedback)
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
    }
}
