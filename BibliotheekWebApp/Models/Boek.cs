using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class Boek
    {
        [Key]
        [Required]
        public string ISBN { get; set; }

        [Required]
        [StringLength(200)]
        public string Titel { get; set; }

        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicatieDatum { get; set; }

        public int? AuteurID { get; set; }
        public Auteur Auteur { get; set; }

        public bool IsDeleted { get; set; }

        public List<LidBoek> LidBoeken { get; set; } = new List<LidBoek>();
    }
}





