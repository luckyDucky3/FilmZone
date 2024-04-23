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
        Task<Film> GetByName (string name);
    }
}
