using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private IEnumerable<Event> _events = new Event[] {
            new Event() {
                EventId = 1,
                Theme = "Angular 11",
                Place = "Rua 1",
                NumberOfPerson = 200,
                EventDate = DateTime.Now.AddDays(4).ToString(),
                ImageUrl = "photoAngular.png"
            },
            new Event() {
                EventId = 2,
                Theme = "Java 12",
                Place = "Rua 2",
                NumberOfPerson = 400,
                EventDate = DateTime.Now.AddDays(3).ToString(),
                ImageUrl = "photoJava.png"
            }
        };
        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _events;
        }

        [HttpGet("{id}")]
        public IEnumerable<Event> GetById(int id)
        {
            return _events.Where(Event => Event.EventId == id);
        }
    }
}
