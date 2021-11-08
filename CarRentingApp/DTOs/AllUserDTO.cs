using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentingApp.DTOs
{
    public class AllUserDTO
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
