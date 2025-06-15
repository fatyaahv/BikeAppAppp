using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeAppApp.Models
{
    [Table("Alicilar")]
    public partial class Alicilar
    {
        public Alicilar()
        {
            BayiAlicis = new HashSet<BayiAlici>();
        }

        [Key]
        [Column("AliciID")]
        public int AliciId { get; set; }
        [StringLength(100)]
        public string? Isim { get; set; }
        [StringLength(100)]
        public string? Soyisim { get; set; }
        [StringLength(20)]
        public string? Telefon { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }

        [InverseProperty("Alici")]
        public virtual ICollection<BayiAlici> BayiAlicis { get; set; }
    }
}
