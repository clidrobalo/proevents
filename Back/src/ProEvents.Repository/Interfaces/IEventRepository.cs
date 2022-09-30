using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Repository.Interfaces
{
    public interface IEventRepository
    {
        Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false);
        Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false);
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false);
    }
}