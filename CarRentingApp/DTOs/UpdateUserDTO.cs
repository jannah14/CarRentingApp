using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingApp.DTOs
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string Lastname { get; set; }
        
        [DataType(DataType.Date)]
        [Remote(action: "VerifyUpdateBirthday", controller: "User")]
        public DateTime Birthday { get; set; }
        
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public IEnumerable<string> AllRoles { get; set; }
    }
}
