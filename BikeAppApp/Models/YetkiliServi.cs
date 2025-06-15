using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    public partial class YetkiliServi
    {
        public YetkiliServi()
        {
            BakimGecmisis = new HashSet<BakimGecmisi>();
            Calisanlars = new HashSet<Calisanlar>();
        }

        [Key]
        [Column("ServisID")]
        public int ServisId { get; set; }
        [StringLength(100)]
        public string? ServisAdi { get; set; }
        [StringLength(200)]
        public string? Adres { get; set; }

        [InverseProperty("Servis")]
        public virtual ICollection<BakimGecmisi> BakimGecmisis { get; set; }
        [InverseProperty("CalistigiYerNavigation")]
        public virtual ICollection<Calisanlar> Calisanlars { get; set; }
    }
}
