using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmZone.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<T> GetById(int id);

        Task<List<T>> Select();

        Task<bool> Delete(T entity);

        Task<bool> Update(T entity);
    }
}
