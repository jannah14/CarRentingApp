using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.DTOs
{
    public class EditRentalDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Remote(action: "VerifyStartDate", controller: "Rentals")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Remote(action: "VerifyEndDate", controller: "Rentals", AdditionalFields =nameof(StartDate))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Vehicle")]
        [Remote(action: "VerifyVehicle", controller: "Rentals", AdditionalFields = nameof(StartDate) + "," + nameof(EndDate))]
        public int VehicleId { get; set; }
        public byte Status { get; set; }
        public string RejectionReason { get; set; }
    }
}
