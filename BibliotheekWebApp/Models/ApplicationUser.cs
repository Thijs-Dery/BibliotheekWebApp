using Microsoft.AspNetCore.Identity;

namespace BibliotheekApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
    }
}
