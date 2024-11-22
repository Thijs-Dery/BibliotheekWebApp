using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotheekApp.Models
{
    public class LidBoek
    {
        [Key]
        public int LidBoekID { get; set; }

        [ForeignKey("Lid")]
        public int? LidID { get; set; }
        public Lid Lid { get; set; }

        [ForeignKey("Boek")]
        [Column("ISBN")]
        public string ISBN { get; set; }
        public Boek Boek { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UitleenDatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime? InleverDatum { get; set; }
    }
}

