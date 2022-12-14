using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extentions;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Repository.Models;

namespace ProEvents.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(IEventsService eventService, IWebHostEnvironment webHostEnvironment)
        {
            this._eventsService = eventService;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents([FromQuery] PageParams pageParams)
        {
            try
            {
                Thread.Sleep(1000); // delay for spinner in frontend
                var Events = await _eventsService.GetAllEventsAsync(User.GetUserId(), pageParams);

                if (Events == null)
                {
                    return NoContent();
                }

                Response.AddPagination(Events.CurrentPage, Events.PageSize, Events.TotalCount, Events.TotalPages);

                return Ok(Events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Get Events. Error: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Thread.Sleep(1000); // delay for spinner in frontend
                var Event = await _eventsService.GetEventByIdAsync(User.GetUserId(), id);

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

        [HttpPost]
        public async Task<IActionResult> Save(EventDTO model)
        {
            try
            {
                var Event = await _eventsService.AddEvent(model);

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

        [HttpPost("upload-image/{eventId}")]
        public async Task<IActionResult> UploadImage(int eventId)
        {
            try
            {
                var _event = await _eventsService.GetEventByIdAsync(User.GetUserId(), eventId);

                if (_event == null)
                {
                    return NoContent();
                }

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(_event.ImageUrl);
                    _event.ImageUrl = await SaveImage(file);
                }

                _event = await _eventsService.UpdateEvent(_event);

                return Ok(_event);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Upload Image. Error: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(EventDTO model)
        {
            try
            {
                var Event = await _eventsService.UpdateEvent(model);

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
                var userId = User.GetUserId();
                var _event = await _eventsService.GetEventByIdAsync(userId, id);

                if (_event != null && (await _eventsService.DeleteEvent(userId, id)))
                {
                    DeleteImage(_event.ImageUrl);
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

        [NonAction]
        private async Task<string> SaveImage(IFormFile file)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(file.FileName)
            //.Take(10) // take the first 10 characters
            .ToArray())
            .Replace(' ', '-');

            imageName = $"{imageName}__{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources/Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        private void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"Resources/Images", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }


}
