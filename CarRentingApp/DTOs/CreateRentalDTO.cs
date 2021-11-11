using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.DTOs
{
    public class CreateRentalDTO
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public string AppUserId { get; set; }

        public int TotalPrice { get; set; }

        public int VehicleId { get; set; }
    }
}
