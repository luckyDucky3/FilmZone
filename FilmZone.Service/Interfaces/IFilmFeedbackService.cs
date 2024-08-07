using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FilmZone.Service.Interfaces
{
    public interface IFilmFeedbackService
    {
        Task<IBaseResponse<bool>> CreateFeedback(FilmFeedback feedback);
        Task<IBaseResponse<List<FilmFeedback>>> GetListOfFeedback(int filmId);
        Task<IBaseResponse<FilmFeedback>> GetFeedbackByLoginAndFilmName(string login, int filmId);
        Task<IBaseResponse<bool>> UpdateFeedbackRating(string login, int filmId, int rating);
    }
}
