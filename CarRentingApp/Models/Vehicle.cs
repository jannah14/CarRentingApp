using CarRentingApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; }
        public int ItemsInStock { get; set; }
        public double PricePerDay { get; set; }

        [Required]
        public string AgentId { get; set; } //foreign key with AppUsers

        public int VehicleModelId { get; set; } //foreign key with VehicleModel

        public AppUser Agent { get; set; }
        public VehicleModel VehicleModel { get; set; }

    }
}
