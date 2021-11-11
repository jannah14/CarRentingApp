using CarRentingApp.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.Rental
{
    public class CreateRentalDTOValidator : AbstractValidator<CreateRentalDTO>
    {
        public CreateRentalDTOValidator()
        {
            RuleFor(r => r.StartDate).NotNull().Must(sd => sd > DateTime.Now);
            RuleFor(r => r.EndDate).NotNull().Must(sd => sd > DateTime.Now);
            RuleFor(r => r.AppUserId).NotNull().NotEmpty();
            RuleFor(r => r.TotalPrice).Must(n => n > 0);
            RuleFor(r => r.VehicleId).Must(n => n > 0);
        }
    }
}
