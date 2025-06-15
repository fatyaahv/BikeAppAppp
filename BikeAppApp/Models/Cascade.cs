using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BikeAppApp.Models
{
    public class Cascade
    {
        public IEnumerable<SelectListItem> CCList { get; set; }
        public IEnumerable<SelectListItem> MotorList { get; set; }
        public int SelectedCC { get; set; }
        public int SelectedMotor { get; set; }
    }
}
