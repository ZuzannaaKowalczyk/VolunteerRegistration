using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa wydarzenia")]
        [Required(ErrorMessage = "Nazwa wydarzenia jest wymagana")]
        [MaxLength(100)]
        public string EventName { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis wydarzenia jest wymagany")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Data wydarzenia")]
        [Required(ErrorMessage = "Data wydarzenia jest wymagana")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Lokalizacja")]
        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        [MaxLength(100)]
        public string Location { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
}
