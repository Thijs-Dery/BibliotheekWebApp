using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class Boek
    {
        [Key]
        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Titel { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime PublicatieDatum { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Required]
        public int AuteurID { get; set; }

        public Auteur? Auteur { get; set; } = null;

        public ICollection<LidBoek> LidBoeken { get; set; } = new List<LidBoek>();
    }
}
