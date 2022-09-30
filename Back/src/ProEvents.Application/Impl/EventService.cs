using System;
using System.Threading.Tasks;
using ProEvents.Application.Interfaces;
using ProEvents.Domain;
using ProEvents.Repository.Interfaces;

namespace ProEvents.Application.Impl
{
    public class EventService : IEventService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository, IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
            _eventRepository = eventRepository;
        }
        public async Task<Event> AddEvent(Event model)
        {
            try
            {
                _genericRepository.Add<Event>(model);
                if (await _genericRepository.SaveChangesAsync())
                {
                    return await _eventRepository.GetEventByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> UpdateEvent(int eventId, Event model)
        {
            try
            {
                var Event = await _eventRepository.GetEventByIdAsync(eventId);

                if (Event == null) return null;

                _genericRepository.Update(model);

                if (await _genericRepository.SaveChangesAsync())
                {
                    return await _eventRepository.GetEventByIdAsync(eventId);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                var Event = await _eventRepository.GetEventByIdAsync(eventId);

                if (Event == null) throw new Exception("Event not found.");

                _genericRepository.Delete(Event);

                return await _genericRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            try
            {
                return await _eventRepository.GetAllEventsAsync(includeSpeakers);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers = false)
        {
            try
            {
                return await _eventRepository.GetAllEventsByThemeAsync(theme, includeSpeakers);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            try
            {
                return await _eventRepository.GetEventByIdAsync(eventId, includeSpeakers);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}