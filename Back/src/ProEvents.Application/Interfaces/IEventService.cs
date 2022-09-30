using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Application.Interfaces
{
    public interface IEventService
    {
        Task<Event> AddEvent(Event model);
        Task<Event> UpdateEvent(int eventId, Event model);
        Task<bool> DeleteEvent(int eventId);
        Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false);
        Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false);
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false);
    }
}