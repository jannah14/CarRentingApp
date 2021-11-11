using CarRentingApp.Controllers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.Rental
{
    public class IntIdentifierValidator : AbstractValidator<IntIdentifier>
    {

        public IntIdentifierValidator()
        {
            RuleFor(n => n.Id).NotNull().Must(n => n > 0);
        }
    }
}
