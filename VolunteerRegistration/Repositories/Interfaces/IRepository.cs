using System.Linq;
using System.Threading.Tasks;
namespace VolunteerRegistration.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T model);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}

