using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.DAL.Interfaces
{
    public interface IFilmFeedbackRepository : IBaseRepository<FilmFeedback>
    {
        Task<List<FilmFeedback>> GetFeedbacks(int filmId);
        Task<FilmFeedback> GetFeedbackByLoginAndFilmName(string login, int filmId);
        Task<bool> UpdateFeedback(string login, int filmId, int rating);
        Task<bool> UpdateEmptyFeedbackWithRating(string login, int filmId, FilmFeedback feedback);
        Task<List<float>> GetListOfValues(int filmId);
    }
}
