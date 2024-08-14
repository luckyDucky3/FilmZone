using FilmZone.Domain.Enum;
using FilmZone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmZone.DAL.Interfaces
{
    public interface IFilmRepository : IBaseRepository<Film>
    {
        Task<Film?> GetByName (string name);
        Task<List<Film>> GetByType(TypeFilm type);
        Task<List<Film>> GetFilmOrSerialsByRating(int countFilm);
        Task<List<Film>> GetFilmsByReleaseDate(int countFilm);
        Task<List<Film>> GetSerialsByReleaseDate(int countFilm);
    }
}
