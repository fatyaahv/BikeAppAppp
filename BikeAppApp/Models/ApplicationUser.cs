using Microsoft.AspNetCore.Identity;
using System;

namespace BikeAppApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom properties added for your users:
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }

    
    }
}
