using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Repository.Contexts;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Repository.Impl
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ProEventsContext _proEventsContext;

        public SpeakerRepository(ProEventsContext proEventsContext)
        {
            this._proEventsContext = proEventsContext;
        }
        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _proEventsContext.Speakers
           .Include(e => e.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Event);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents = false)
        {
            IQueryable<Speaker> query = _proEventsContext.Speakers
           .Include(e => e.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Event);
            }

            query = query.AsNoTracking().Where(e => e.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents = false)
        {
            IQueryable<Speaker> query = _proEventsContext.Speakers
          .Include(e => e.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(e => e.EventSpeakers)
                .ThenInclude(ev => ev.Event);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}