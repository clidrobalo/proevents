using System.Collections.Generic;

namespace ProEvents.Application.Dtos
{
    public class SpeakerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }
        public IEnumerable<EventDTO> Events { get; set; }
    }
}