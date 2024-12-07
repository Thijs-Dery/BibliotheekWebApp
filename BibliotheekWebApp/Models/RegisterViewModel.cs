using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens {2} en maximaal {1} tekens lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Voornaam { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Achternaam { get; set; }
    }
}
