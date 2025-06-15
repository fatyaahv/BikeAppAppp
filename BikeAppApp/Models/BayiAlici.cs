using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    [Table("BayiAlici")]
    public partial class BayiAlici
    {
        [Key]
        [Column("BayiAliciID")]
        public int BayiAliciId { get; set; }
        [Column("BayiID")]
        public int? BayiId { get; set; }
        [Column("AliciID")]
        public int? AliciId { get; set; }
        [Column("MotosikletID")]
        public int? MotosikletId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SatisTarihi { get; set; }

        [ForeignKey("AliciId")]
        [InverseProperty("BayiAlicis")]
        public virtual Alicilar? Alici { get; set; }
        [ForeignKey("BayiId")]
        [InverseProperty("BayiAlicis")]
        public virtual Bayiler? Bayi { get; set; }
        [ForeignKey("MotosikletId")]
        [InverseProperty("BayiAlicis")]
        public virtual Motosikletler? Motosiklet { get; set; }
    }
}
