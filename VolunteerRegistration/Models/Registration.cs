namespace VolunteerRegistration.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }


        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
