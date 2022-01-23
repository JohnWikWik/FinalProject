using System;
using System.ComponentModel.DataAnnotations;

namespace VaccineRegistration.Models
{
    public class IsGreaterDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime VaccineDate = Convert.ToDateTime(value);
            if(VaccineDate > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
