using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Application.Impl
{
    public class LoteService : ILoteService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly ILoteRepository _loteRepository;

        public LoteService(ILoteRepository loteRepository, IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _loteRepository = loteRepository;
            _mapper = mapper;
        }

        public async Task<LoteDTO> GetLoteByIdAsync(int loteId)
        {
            try
            {
                var Lote = await _loteRepository.GetLoteByIdAsync(loteId);

                if (Lote != null)
                {
                    return _mapper.Map<LoteDTO>(Lote);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LoteDTO[]> GetLotesByEventIdAsync(int eventId)
        {
            try
            {
                var Lotes = await _loteRepository.GetLotesByEventIdAsync(eventId);

                if (Lotes != null)
                {
                    return _mapper.Map<LoteDTO[]>(Lotes);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LoteDTO[]> GetAllLotesAsync()
        {
            try
            {
                var _lotes = await _loteRepository.GetAllLotesAsync();

                if (_lotes != null)
                {
                    return _mapper.Map<LoteDTO[]>(_lotes);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LoteDTO[]> AddOrUpdateLotes(int eventId, LoteDTO[] loteDTOs)
        {
            try
            {
                var _lotes = this._mapper.Map<Lote[]>(loteDTOs);

                foreach (var lote in _lotes)
                {
                    lote.EventId = eventId;
                    // Save a new One
                    if (lote.Id == 0)
                    {
                        _genericRepository.Add<Lote>(lote);
                    }
                    else
                    {
                        var _lote = await _loteRepository.GetLoteByIdAsync(lote.Id);

                        if (_lote == null) return null;

                        _genericRepository.Update(lote);
                    }
                    await _genericRepository.SaveChangesAsync();
                }

                _lotes = await _loteRepository.GetLotesByEventIdAsync(eventId);

                if (_lotes != null && _lotes.Length > 0)
                {
                    return this._mapper.Map<LoteDTO[]>(_lotes);
                }

                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteLote(int loteId)
        {
            try
            {
                var _lote = await _loteRepository.GetLoteByIdAsync(loteId);

                if (_lote == null) throw new Exception("Lote not found.");

                _genericRepository.Delete(_lote);

                return await _genericRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}