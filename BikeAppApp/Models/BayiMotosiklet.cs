using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Models
{
    [Table("BayiMotosiklet")]
    public partial class BayiMotosiklet
    {
        [Key]
        [Column("BayiMotosikletID")]
        public int BayiMotosikletId { get; set; }
        [Column("BayiID")]
        public int? BayiId { get; set; }
        [Column("MotosikletID")]
        public int? MotosikletId { get; set; }

        [ForeignKey("BayiId")]
        [InverseProperty("BayiMotosiklets")]
        public virtual Bayiler? Bayi { get; set; }
        [ForeignKey("MotosikletId")]
        [InverseProperty("BayiMotosiklets")]
        public virtual Motosikletler? Motosiklet { get; set; }
    }
}
