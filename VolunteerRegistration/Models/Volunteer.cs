using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [MaxLength(20)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Imię nie może być puste")]
        public string Firstname { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwisko nie może być puste")]
        public string Lastname { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Data urodzenia")]
        [Required(ErrorMessage = "Data urodzenia jest wymagana")]
        public DateTime BirthDate { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
