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

        [Required(ErrorMessage = "İsim is required.")]
        [StringLength(100, ErrorMessage = "İsim cannot be longer than 100 characters.")]
        public string Isim { get; set; } = null!;

        [Required(ErrorMessage = "Soyisim is required.")]
        [StringLength(100, ErrorMessage = "Soyisim cannot be longer than 100 characters.")]
        public string Soyisim { get; set; } = null!;

        [Required(ErrorMessage = "Telefon is required.")]
        [Phone(ErrorMessage = "Telefon must be a valid phone number.")]
        [StringLength(20, ErrorMessage = "Telefon cannot be longer than 20 characters.")]
        public string Telefon { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; } = null!;

        [InverseProperty("Alici")]
        public virtual ICollection<BayiAlici> BayiAlicis { get; set; }
    }
}