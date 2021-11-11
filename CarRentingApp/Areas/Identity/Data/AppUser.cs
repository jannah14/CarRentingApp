using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarRentingApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    public class AppUser : IdentityUser
    {
        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string DrivingLicense { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}
