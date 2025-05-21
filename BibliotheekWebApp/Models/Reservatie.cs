namespace BibliotheekWebApp.Models
{
    public class Reservatie
    {
        public int Id { get; set; }
        public string GebruikerId { get; set; }  // Link naar Identity User
        public int BoekId { get; set; }
        public DateTime ReservatieDatum { get; set; }
        public bool Verwerkt { get; set; }
    }

}
