using System.Threading.Tasks;
using ProEvents.Application.Dtos;
using ProEvents.Repository.Models;

namespace ProEvents.Application.Interfaces
{
    public interface IEventsService
    {
        Task<EventDTO> AddEvent(EventDTO model);
        Task<EventDTO> UpdateEvent(EventDTO model);
        Task<bool> DeleteEvent(int userId, int eventId);
        Task<PageList<EventDTO>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeSpeakers = false);
        Task<EventDTO> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false);
    }
}