using System.Threading.Tasks;
using ProEvents.Application.Dtos;

namespace ProEvents.Application.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDTO> GetLoteByIdAsync(int loteId);
        Task<LoteDTO[]> GetLotesByEventIdAsync(int loteId);
        Task<LoteDTO[]> GetAllLotesAsync();
        Task<LoteDTO[]> AddOrUpdateLotes(int eventId, LoteDTO[] models);
        Task<bool> DeleteLote(int loteId);
    }
}