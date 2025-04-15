using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class EventOrganizer
    {
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; }
    }
}
