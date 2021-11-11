using CarRentingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.ViewModels
{
    public class EditRentalViewModel
    {
        public EditRentalDTO Rental { get; set; }

        public IEnumerable<MinVehicleDTO> Vehicles { get; set; }
    }
}
