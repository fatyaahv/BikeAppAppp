using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    [Table("BakimGecmisi")]
    public partial class BakimGecmisi
    {
        [Key]
        [Column("BakimID")]
        public int BakimId { get; set; }
        [Column("MotosikletID")]
        public int? MotosikletId { get; set; }
        [Column("ServisID")]
        public int? ServisId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BakimTarihi { get; set; }
        [StringLength(500)]
        public string? YapilanIslemler { get; set; }

        [ForeignKey("MotosikletId")]
        [InverseProperty("BakimGecmisis")]
        public virtual Motosikletler? Motosiklet { get; set; }
        [ForeignKey("ServisId")]
        [InverseProperty("BakimGecmisis")]
        public virtual YetkiliServis? Servis { get; set; }
    }
}
