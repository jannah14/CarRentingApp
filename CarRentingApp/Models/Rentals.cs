using CarRentingApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Models
{
    public class Rentals
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float TotalPrice { get; set; }
        public byte Status { get; set; }
        
        [StringLength(255)]
        public string RejectionReason { get; set; }

        public string AppUserId { get; set; }  //foreign key with AppUser
        public int VehicleId { get; set; } //foreign key with Vehicle

        public AppUser AppUser { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public enum RentalStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
