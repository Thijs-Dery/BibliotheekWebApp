using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class Auteur
    {
        [Key]
        public int AuteurID { get; set; }

        [Required]
        [StringLength(200)]
        public string Naam { get; set; }

        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<Boek> Boeken { get; set; }
    }
}





