using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Brand { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        public byte Type { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Van,
        SUV
    }
}
