namespace VolunteerRegistration.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}


