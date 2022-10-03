using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                //Thread.Sleep(1000); // delay for spinner in frontend
                var Events = await _eventService.GetAllEventsAsync();

                if (Events == null)
                {
                    return NoContent();
                }

                return Ok(Events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Events. Error: {e.Message}");
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Event = await _eventService.GetEventByIdAsync(id);

                if (Event == null)
                {
                    return NoContent();
                }
                return Ok(Event);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Event By Id. Error: {e.Message}");
            }
        }

        [HttpGet("theme/{theme}")]
        public async Task<IActionResult> GetByTheme(string theme)
        {
            try
            {
                var Events = await _eventService.GetAllEventsByThemeAsync(theme);

                if (Events == null)
                {
                    return NoContent();
                }
                return Ok(Events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Events By Theme. Error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(EventDTO model)
        {
            try
            {
                var Event = await _eventService.AddEvent(model);

                if (Event == null)
                {
                    return NoContent();
                }
                return Ok(Event);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Save Event. Error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EventDTO model)
        {
            try
            {
                var Event = await _eventService.UpdateEvent(id, model);

                if (Event == null)
                {
                    return NoContent();
                }
                return Ok(Event);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Update Event. Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var wasDeleted = await _eventService.DeleteEvent(id);

                if (wasDeleted)
                {
                    return Ok("Event Deleted");
                }
                return BadRequest("Event not Deleted.");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Delete Event. Error: {e.Message}");
            }
        }
    }
}
