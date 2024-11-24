using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotheekApp.Models
{
    public class LidBoek
    {
        [Key]
        public int LidBoekID { get; set; }

        public int LidID { get; set; }
        public Lid Lid { get; set; }

        [Required]
        public string ISBN { get; set; }
        public Boek Boek { get; set; }

        public DateTime UitleenDatum { get; set; }
        public DateTime InleverDatum { get; set; }
    }
}



