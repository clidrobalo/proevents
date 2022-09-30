using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Repository.Impl
{
    public class EventRepository : IEventRepository
    {
        private readonly ProEventsContext _proEventsContext;
        public EventRepository(ProEventsContext proEventsContext)
        {
            this._proEventsContext = proEventsContext;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers)
        {
            IQueryable<Event> query = _proEventsContext.Events
            .Include(e => e.Lotes)
            .Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers)
        {
            IQueryable<Event> query = _proEventsContext.Events
             .Include(e => e.Lotes)
             .Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker);
            }

            query = query.AsNoTracking().Where(e => e.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers)
        {
            IQueryable<Event> query = _proEventsContext.Events
             .Include(e => e.Lotes)
             .Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}