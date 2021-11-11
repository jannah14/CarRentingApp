using CarRentingApp.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.Rental
{
    public class EditRentalDTOValidator : AbstractValidator<EditRentalDTO>
    {
        public EditRentalDTOValidator()
        {
            RuleFor(r => r.Id).NotNull().Must(r => r > 0);
            RuleFor(r => r.VehicleId).NotNull().Must(r => r > 0);
        }
    }
}
