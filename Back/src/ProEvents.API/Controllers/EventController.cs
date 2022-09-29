using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEvents.API.Data;
using ProEvents.API.Models;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly DataContext _dbcontext;
        public EventController(DataContext context)
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
            return _dbcontext.Events.FirstOrDefault(Event => Event.EventId == id);
        }
    }
}
