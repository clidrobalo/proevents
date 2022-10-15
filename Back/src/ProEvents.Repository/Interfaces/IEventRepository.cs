using System.Threading.Tasks;
using ProEvents.Domain;
using ProEvents.Repository.Models;

namespace ProEvents.Repository.Interfaces
{
    public interface IEventRepository
    {
        Task<PageList<Event>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeSpeakers = false);
        Task<Event> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false);
    }
}