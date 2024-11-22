using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotheekApp.Models
{
    public class Lid
    {
        [Key]
        public int LidID { get; set; }

        [Required]
        [StringLength(100)]
        public string Naam { get; set; }

        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; }

        public List<LidBoek> GeleendeBoeken { get; set; } = new List<LidBoek>();
    }
}


