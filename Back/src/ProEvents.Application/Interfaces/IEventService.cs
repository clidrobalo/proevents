using System.Threading.Tasks;
using ProEvents.Application.Dtos;

namespace ProEvents.Application.Interfaces
{
    public interface IEventService
    {
        Task<EventDTO> AddEvent(EventDTO model);
        Task<EventDTO> UpdateEvent(EventDTO model);
        Task<bool> DeleteEvent(int eventId);
        Task<EventDTO[]> GetAllEventsAsync(bool includeSpeakers = false);
        Task<EventDTO> GetEventByIdAsync(int eventId, bool includeSpeakers = false);
        Task<EventDTO[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false);
    }
}