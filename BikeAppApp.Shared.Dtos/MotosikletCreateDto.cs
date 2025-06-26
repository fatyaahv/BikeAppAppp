using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeAppApp.Shared.Dtos
{
    public class MotosikletCreateDto
    {
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public decimal Fiyat { get; set; }
        // Add other properties if needed
    }
}
