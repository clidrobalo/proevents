using System.Collections.Generic;
using ProEvents.Domain.Identity;

namespace ProEvents.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Resume { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}