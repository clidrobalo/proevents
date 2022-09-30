using System.Threading.Tasks;

namespace ProEvents.Repository.Interfaces
{
    public interface IGenericRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entities) where T : class;
        Task<bool> SaveChangesAsync();
    }
}