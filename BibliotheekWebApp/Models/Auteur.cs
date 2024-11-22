using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class Auteur
    {
        public int AuteurID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naam { get; set; }

        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; }

        public List<Boek> Boeken { get; set; } = new List<Boek>();
    }
}

