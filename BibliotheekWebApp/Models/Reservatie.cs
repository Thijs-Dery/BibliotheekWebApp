namespace BibliotheekWebApp.Models
{
    public class Reservatie
    {
        public int Id { get; set; }
        public string GebruikerId { get; set; }
        public string ISBN { get; set; }
        public DateTime ReservatieDatum { get; set; }
        public bool Verwerkt { get; set; }
    }

}
