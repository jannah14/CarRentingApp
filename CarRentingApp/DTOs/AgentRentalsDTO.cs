using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.DTOs
{
    public class AgentRentalsDTO
    {
        public int Id { get; set; }

        public string Vehicle { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string User { get; set; }


    }
}
