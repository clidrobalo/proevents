using System.Collections.Generic;

namespace ProEvents.Application.Dtos
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int NumberOfPerson { get; set; }
        public string ImageUrl { get; set; }
        public string phone { get; set; }
        public IEnumerable<LoteDTO> Lotes { get; set; }
        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }
        public IEnumerable<SpeakerDTO> Speakers { get; set; }
    }
}