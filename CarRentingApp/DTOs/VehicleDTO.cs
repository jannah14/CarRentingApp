using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.DTOs
{
    public class VehicleDTO : MinVehicleDTO
    {
        public double PricePerDay { get; set; }
    }

    public class MinVehicleDTO
    {
        public int Id { get; set; }
        public string BrandModel { get; set; }  //return the color brand and model of each vehicle
    }
}
