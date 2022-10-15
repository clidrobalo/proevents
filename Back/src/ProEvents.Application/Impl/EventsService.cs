using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Repository.Interfaces;
using ProEvents.Repository.Models;

namespace ProEvents.Application.Impl
{
    public class EventsService : IEventsService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IEventRepository _eventRepository;

        private readonly IMapper _mapper;

        public EventsService(IEventRepository eventRepository, IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<PageList<EventDTO>> GetAllEventsAsync(int userId, PageParams pageParams, bool includeSpeakers = false)
        {
            try
            {
                var Events = await _eventRepository.GetAllEventsAsync(userId, pageParams, includeSpeakers);

                if (Events != null)
                {
                    var result = _mapper.Map<PageList<EventDTO>>(Events);

                    result.CurrentPage = Events.CurrentPage;
                    result.TotalPages = Events.TotalPages;
                    result.TotalCount = Events.TotalCount;
                    result.PageSize = Events.PageSize;

                    return result;
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDTO> GetEventByIdAsync(int userId, int eventId, bool includeSpeakers = false)
        {
            try
            {
                var Event = await _eventRepository.GetEventByIdAsync(userId, eventId, includeSpeakers);

                if (Event != null)
                {
                    return _mapper.Map<EventDTO>(Event);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDTO> AddEvent(EventDTO eventDTO)
        {
            try
            {
                var Event = this._mapper.Map<Event>(eventDTO);

                _genericRepository.Add<Event>(Event);
                if (await _genericRepository.SaveChangesAsync())
                {
                    Event = await _eventRepository.GetEventByIdAsync(eventDTO.UserId, Event.Id);
                    return this._mapper.Map<EventDTO>(Event);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDTO> UpdateEvent(EventDTO eventDTO)
        {
            try
            {
                var Event = await _eventRepository.GetEventByIdAsync(eventDTO.UserId, eventDTO.Id);

                if (Event == null) return null;

                _genericRepository.Update(this._mapper.Map<Event>(eventDTO));

                if (await _genericRepository.SaveChangesAsync())
                {
                    Event = await _eventRepository.GetEventByIdAsync(eventDTO.UserId, eventDTO.Id);
                    return this._mapper.Map<EventDTO>(Event);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvent(int userId, int eventId)
        {
            try
            {
                var Event = await _eventRepository.GetEventByIdAsync(userId, eventId);

                if (Event == null) throw new Exception("Event not found.");

                _genericRepository.Delete(Event);

                return await _genericRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}