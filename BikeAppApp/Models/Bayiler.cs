using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    [Table("Bayiler")]
    public partial class Bayiler
    {
        public Bayiler()
        {
            BayiAlicis = new HashSet<BayiAlici>();
            BayiMotosiklets = new HashSet<BayiMotosiklet>();
            Calisanlars = new HashSet<Calisanlar>();
        }

        [Key]
        [Column("BayiID")]
        public int BayiId { get; set; }
        [StringLength(100)]
        public string? BayiAdi { get; set; }
        [StringLength(200)]
        public string? Adres { get; set; }

        [InverseProperty("Bayi")]
        public virtual ICollection<BayiAlici> BayiAlicis { get; set; }
        [InverseProperty("Bayi")]
        public virtual ICollection<BayiMotosiklet> BayiMotosiklets { get; set; }
        [InverseProperty("CalistigiYer")]
        public virtual ICollection<Calisanlar> Calisanlars { get; set; }
    }
}
