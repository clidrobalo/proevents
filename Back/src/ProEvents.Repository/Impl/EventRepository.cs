using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;
using ProEvents.Repository.Models;

namespace ProEvents.Repository.Impl
{
    public class EventRepository : IEventRepository
    {
        private readonly ProEventsContext _proEventsContext;
        public EventRepository(ProEventsContext proEventsContext)
        {
            this._proEventsContext = proEventsContext;
        }

        public async Task<PageList<Event>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeSpeakers)
        {
            IQueryable<Event> query = _proEventsContext.Events
            .Include(e => e.Lotes)
            .Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker);
            }

            query = query.AsNoTracking().Where(e => e.Theme.ToLower().Contains(pageParams.Term.ToLower()) && e.UserId == userId).OrderBy(e => e.Id);

            return await PageList<Event>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }
        public async Task<Event> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers)
        {
            IQueryable<Event> query = _proEventsContext.Events
             .Include(e => e.Lotes)
             .Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Speaker);
            }

            query = query.AsNoTracking().Where(e => e.Id == eventId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}