using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    [Table("Calisanlar")]
    public partial class Calisanlar
    {
        [Key]
        [Column("CalisanID")]
        public int CalisanId { get; set; }
        [StringLength(100)]
        public string? Isim { get; set; }
        [StringLength(100)]
        public string? Soyisim { get; set; }
        [StringLength(20)]
        public string? Telefon { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [Column("CalistigiYerID")]
        public int? CalistigiYerId { get; set; }
        [StringLength(50)]
        public string? CalistigiYerTipi { get; set; }

        [ForeignKey("CalistigiYerId")]
        [InverseProperty("Calisanlars")]
        public virtual Bayiler? CalistigiYer { get; set; }
        [ForeignKey("CalistigiYerId")]
        [InverseProperty("Calisanlars")]
        public virtual YetkiliServis? CalistigiYerNavigation { get; set; }
    }
}
