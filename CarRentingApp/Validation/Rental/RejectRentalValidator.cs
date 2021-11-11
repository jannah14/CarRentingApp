using CarRentingApp.Controllers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.Rental
{
    public class RejectRentalValidator: AbstractValidator<RejectRental>
    {
        public RejectRentalValidator()
        {
            RuleFor(r => r.RentalId).NotNull().Must(r => r > 0);
            RuleFor(a => a.RejectionReason).NotEmpty().NotNull();
        }
    }
}
