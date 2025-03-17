using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace VolunteerRegistration.Models
{
    public class VolunteerRegistrationContext : DbContext
    {
        public VolunteerRegistrationContext(DbContextOptions<VolunteerRegistrationContext> options) : base(options) {  }

        public VolunteerRegistrationContext() { }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EventOrganizer> EventOrganizers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Volunteer → Registration
            modelBuilder.Entity<Volunteer>()
                .HasMany(v => v.Registrations)
                .WithOne(r => r.Volunteer)
                .HasForeignKey(r => r.VolunteerId);

            // One-to-Many: Event → Registration
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Registrations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId);

            // Many-to-Many: Event ↔ Organizer (tabela pośrednia EventOrganizer)
            modelBuilder.Entity<EventOrganizer>()
                .HasKey(eo => new { eo.EventId, eo.OrganizerId });

            modelBuilder.Entity<EventOrganizer>()
                .HasOne(eo => eo.Event)
                .WithMany(e => e.EventOrganizers)
                .HasForeignKey(eo => eo.EventId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Event może usuwać powiązania

            modelBuilder.Entity<EventOrganizer>()
                .HasOne(eo => eo.Organizer)
                .WithMany(o => o.EventOrganizers)
                .HasForeignKey(eo => eo.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Organizer NIE usuwa powiązań

            base.OnModelCreating(modelBuilder);
        }
    }
}
