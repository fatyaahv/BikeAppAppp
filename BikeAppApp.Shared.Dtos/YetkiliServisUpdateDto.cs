﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeAppApp.Shared.Dtos
{
    public class YetkiliServisUpdateDto
    {
        public int ServisId { get; set; }
        public string ServisAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        // Add other properties if needed
    }
}
