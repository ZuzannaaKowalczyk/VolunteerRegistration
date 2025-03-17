namespace VolunteerRegistration.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
}
