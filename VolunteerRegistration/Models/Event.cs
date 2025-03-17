namespace VolunteerRegistration.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
}


