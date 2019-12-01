using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonDirectory.Shared.Helper_Types.Attributes
{
    public class NameCheckAttribute : ValidationAttribute
    {
        public NameCheckAttribute()
        {
        }

        public string GetErrorMessage() =>
            $"ტექსტი უნდა შეიცავდეს მხოლოდ ქართულ ან ლათინურ ანბანის ასოებს, არ უნდა შეიცავდეს ერთდროულად ლათინურ და ქართულ ასოებს";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string val = value.ToString();
                var isOnlyASCII = new Regex("^[A-Za-z]+$").IsMatch(val);
                var isOnlyGeorgian = new Regex("^[ა-ჰ]+$").IsMatch(val);

                if (!isOnlyASCII && !isOnlyGeorgian)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
    }
}
