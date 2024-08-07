using FilmZone.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.DAL.Interfaces
{
    public interface IBestFilmsRepository : IBaseRepository<BestFilm>
    {
        Task<List<BestFilm>> GetAllUserFilms(string login);
    }
}