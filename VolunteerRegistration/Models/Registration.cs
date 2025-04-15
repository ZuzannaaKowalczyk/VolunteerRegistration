using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Display(Name = "Data rejestracji")]
        [Required(ErrorMessage = "Data rejestracji jest wymagana")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
