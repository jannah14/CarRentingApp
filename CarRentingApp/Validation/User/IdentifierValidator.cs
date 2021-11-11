using CarRentingApp.Controllers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.User
{
    public class IdentifierValidator : AbstractValidator<Identifier>
    {
        public IdentifierValidator()
        {
            RuleFor(i => i.UserId).NotEmpty().NotNull();
        }
    }
}
