using System.Threading.Tasks;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Repository.Impl
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ProEventsContext _proEventsContext;

        public GenericRepository(ProEventsContext proEventsContext)
        {
            this._proEventsContext = proEventsContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _proEventsContext.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _proEventsContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _proEventsContext.Remove(entity);
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
            _proEventsContext.RemoveRange(entities);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _proEventsContext.SaveChangesAsync()) > 0;
        }
    }
}