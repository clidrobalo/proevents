using System;
using System.Collections.Generic;

namespace ProEvents.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public DateTime? EventDate { get; set; }
        public string Theme { get; set; }
        public int NumberOfPerson { get; set; }
        public string ImageUrl { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
    }
}
