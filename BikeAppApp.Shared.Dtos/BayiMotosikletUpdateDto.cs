using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeAppApp.Shared.Dtos
{
    public class BayiMotosikletUpdateDto
    {
        public int BayiMotosikletId { get; set; }
        public int BayiId { get; set; }
        public int MotosikletId { get; set; }
        public DateTime SatisTarihi { get; set; }
        // Add other properties if needed
    }
}
