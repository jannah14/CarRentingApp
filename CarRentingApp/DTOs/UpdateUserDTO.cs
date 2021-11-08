using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarRentingApp.DTOs
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        public string Username { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public IEnumerable<string> AllRoles { get; set; }
    }
}
