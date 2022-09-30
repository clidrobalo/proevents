using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Persistence;
using ProEvents.Domain;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ProEventsContext _dbcontext;
        public EventController(ProEventsContext context)
        {
            this._dbcontext = context;
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _dbcontext.Events;
        }

        [HttpGet("{id}")]
        public Event GetById(int id)
        {
            return _dbcontext.Events.FirstOrDefault(Event => Event.Id == id);
        }
    }
}
