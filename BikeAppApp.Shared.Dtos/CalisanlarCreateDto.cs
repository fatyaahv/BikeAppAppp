using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeAppApp.Shared.Dtos
{
    public class CalisanlarCreateDto
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Pozisyon { get; set; }
        public DateTime IseGirisTarihi { get; set; }
        // Add other properties if needed
    }
}
