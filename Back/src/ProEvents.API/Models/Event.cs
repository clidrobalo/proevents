namespace ProEvents.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Place { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int NumberOfPerson { get; set; }
        public string Lote { get; set; }
        public string ImageUrl { get; set; }
    }
}
