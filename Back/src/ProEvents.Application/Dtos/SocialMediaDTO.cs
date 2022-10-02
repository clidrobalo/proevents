namespace ProEvents.Application.Dtos
{
    public class SocialMediaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public EventDTO Event { get; set; }
        public int? SpeakerId { get; set; }
        public SpeakerDTO Speaker { get; set; }
    }
}