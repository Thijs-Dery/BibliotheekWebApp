namespace BibliotheekApp.Models
{
    public class BoekDto
    {
        public string ISBN { get; set; }
        public string Titel { get; set; }
        public string Genre { get; set; }
        public DateTime PublicatieDatum { get; set; }
        public string AuteurNaam { get; set; }
    }
}
