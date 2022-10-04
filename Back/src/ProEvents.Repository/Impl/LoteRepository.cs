using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Repository.Impl
{
    public class LoteRepository : ILoteRepository
    {
        private readonly ProEventsContext _proEventsContext;
        public LoteRepository(ProEventsContext proEventsContext)
        {
            this._proEventsContext = proEventsContext;
        }

        public async Task<Lote> GetLoteByIdAsync(int loteId)
        {
            IQueryable<Lote> query = _proEventsContext.Lotes
             .Include(e => e.Event);

            query = query.AsNoTracking().Where(e => e.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesByEventIdAsync(int eventId)
        {
            IQueryable<Lote> query = _proEventsContext.Lotes
             .Include(e => e.Event);

            query = query.AsNoTracking().Where(e => e.EventId == eventId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote[]> GetAllLotesAsync()
        {
            IQueryable<Lote> query = _proEventsContext.Lotes
            .Include(e => e.Event);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}