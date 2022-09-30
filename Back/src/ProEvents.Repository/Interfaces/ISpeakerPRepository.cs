using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Repository.Interfaces
{
    public interface ISpeakerRepository
    {
        Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents);
        Task<Speaker> GetSpeakerByIdAsync(int eventId, bool includeEvents);
        Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents);
    }
}