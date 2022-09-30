using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

namespace ProEvents.Persistence
{
    public class ProEventsContext : DbContext
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create relation +/+ between Event and Speaker
            modelBuilder.Entity<EventSpeaker>().HasKey(EventSpeaker => new { EventSpeaker.EventId, EventSpeaker.SpeakerId });
        }
    }
}