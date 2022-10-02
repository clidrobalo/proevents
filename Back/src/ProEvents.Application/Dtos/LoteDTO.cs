namespace ProEvents.Application.Dtos
{
    public class LoteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Quantity { get; set; }
        public int EventId { get; set; }
        public EventDTO Event { get; set; }
    }
}