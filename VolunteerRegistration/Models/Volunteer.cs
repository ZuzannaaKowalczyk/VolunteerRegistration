using System.ComponentModel.DataAnnotations;

namespace VolunteerRegistration.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        [Display(Name ="Imię")]
        [MaxLength(20)]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Imię nie może być puste")]
        public string Firstname { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwisko nie może być puste")]
        public string Lastname { get; set; }
        [Display(Name = "Email")] 
        public string Email { get; set; }
        [Display(Name = "Numer telefonu")] 
        public string PhoneNumber { get; set; }
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}


