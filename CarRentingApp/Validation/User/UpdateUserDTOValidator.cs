using CarRentingApp.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Validation.User
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(u => u.Id).NotEmpty().NotNull();
            RuleFor(u => u.Role).NotEmpty().NotNull();
        }
    }
}
