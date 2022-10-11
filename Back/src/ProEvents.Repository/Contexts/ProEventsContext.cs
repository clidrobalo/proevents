using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Domain.Identity;

namespace ProEvents.Repository.Contexts
{
    public class ProEventsContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();


                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            // Create relation +/+ between Event and Speaker
            modelBuilder.Entity<EventSpeaker>().HasKey(EventSpeaker => new { EventSpeaker.EventId, EventSpeaker.SpeakerId });

            modelBuilder.Entity<Event>().HasMany(e => e.SocialMedias)
            .WithOne(rs => rs.Event)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Speaker>().HasMany(e => e.SocialMedias)
            .WithOne(rs => rs.Speaker)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}