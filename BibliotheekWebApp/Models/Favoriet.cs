﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotheekApp.Models
{
    public class Favoriet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GebruikerId { get; set; } = null!;

        [ForeignKey("GebruikerId")]
        public ApplicationUser Gebruiker { get; set; } = null!;

        public string? ISBN { get; set; }

        [ForeignKey("ISBN")]
        public Boek? Boek { get; set; }

        public int? AuteurID { get; set; }

        [ForeignKey("AuteurID")]
        public Auteur? Auteur { get; set; }

        [Required]
        public string Type { get; set; } = null!;
    }
}
