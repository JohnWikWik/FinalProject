using System;
using System.ComponentModel.DataAnnotations;

namespace VaccineRegistration.Models
{
    public class DobValidationAttribute : ValidationAttribute
    {
        int _age;

        public DobValidationAttribute(int age)
        {
            _age = age;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if(DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_age) < DateTime.Now;
            }

            return false;
        }
    }
}
