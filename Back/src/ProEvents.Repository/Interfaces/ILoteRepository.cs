using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Repository.Interfaces
{
    public interface ILoteRepository
    {
        Task<Lote> GetLoteByIdAsync(int loteId);
        Task<Lote[]> GetAllLotesAsync();
        Task<Lote[]> GetLotesByEventIdAsync(int eventId);
    }
}