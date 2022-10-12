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
    public class EventService : IEventService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IEventRepository _eventRepository;

        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDTO[]> GetAllEventsAsync(int userId, bool includeSpeakers = false)
        {
            try
            {
                var Events = await _eventRepository.GetAllEventsAsync(userId, includeSpeakers);

                if (Events != null)
                {
                    return _mapper.Map<EventDTO[]>(Events);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDTO[]> GetAllEventsByThemeAsync(int userId, string theme, bool includeSpeakers = false)
        {
            try
            {
                var Events = await _eventRepository.GetAllEventsByThemeAsync(userId, theme, includeSpeakers);

                if (Events != null)
                {
                    return _mapper.Map<EventDTO[]>(Events);
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