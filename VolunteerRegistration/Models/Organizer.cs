using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class Organizer
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa organizatora")]
        [Required(ErrorMessage = "Nazwa organizatora jest wymagana")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu")]
        public string Phone { get; set; }

        public ICollection<EventOrganizer> EventOrganizers { get; set; } = new List<EventOrganizer>();
    }
}
