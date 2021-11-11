using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.DTOs
{
    public class UserRentalsDTO
    {
        public int Id { get; set; }

        public string VehicleModel { get; set; }  //Color Brand Model
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double TotalPrice { get; set; }

        public byte Status { get; set; }

        public string Agent { get; set; } //Firstname Lastname of the agent

        public int? DaysLeft { get; set; }

        public string RejectionReason { get; set; }
    }
}
