using System.Threading.Tasks;
using ProEvents.Application.Dtos;

namespace ProEvents.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDTO> AddEvent(EventDTO model);
        Task<EventDTO> UpdateEvent(EventDTO model);
        Task<bool> DeleteEvent(int userId, int eventId);
        Task<EventDTO[]> GetAllEventsAsync(int userId, bool includeSpeakers = false);
        Task<EventDTO> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false);
        Task<EventDTO[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false);
    }
}