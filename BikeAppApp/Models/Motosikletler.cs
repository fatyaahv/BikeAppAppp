using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeAppApp.Models
{
    [Table("Motosikletler")]
    public partial class Motosikletler
    {
        public Motosikletler()
        {
            BakimGecmisis = new HashSet<BakimGecmisi>();
            BayiAlicis = new HashSet<BayiAlici>();
            BayiMotosiklets = new HashSet<BayiMotosiklet>();
        }

        [Key]
        [Column("MotosikletID")]
        public int MotosikletId { get; set; }

        [StringLength(50)]
        public string? Marka { get; set; }

        [StringLength(50)]
        public string? Model { get; set; }

        [StringLength(250)]
        public string? FotoğrafYolu { get; set; }

        public int? CC { get; set; } // Nullable int

        [InverseProperty("Motosiklet")]
        public virtual ICollection<BakimGecmisi> BakimGecmisis { get; set; }

        [InverseProperty("Motosiklet")]
        public virtual ICollection<BayiAlici> BayiAlicis { get; set; }

        [InverseProperty("Motosiklet")]
        public virtual ICollection<BayiMotosiklet> BayiMotosiklets { get; set; }
    }
}
