using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CarRentingApp.Validation
{
    public class Min18YearsOld : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (InputModel)validationContext.ObjectInstance;


            var age = DateTime.Today.Year - user.Birthday.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("You must be at least 18 years old.");
        }
    }
}
