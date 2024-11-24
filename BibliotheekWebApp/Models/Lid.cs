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
        [StringLength(200)]
        public string Naam { get; set; }

        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<LidBoek> GeleendeBoeken { get; set; }
    }
}




