using CarRentingApp.DTOs;
using CarRentingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.ViewModels
{
    public class CreateRentalViewModel
    {
        public CreateRentalDTO Rental { get; set; }
        public byte Type { get; set; }
    }
}
