using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int AuteurID { get; set; }
        public Auteur Auteur { get; set; }

        public List<LidBoek> LidBoeken { get; set; } = new List<LidBoek>();
    }

}
