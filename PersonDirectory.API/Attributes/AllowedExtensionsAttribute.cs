using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDirectory.API.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] Extensions)
        {
            _extensions = Extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);
            if (!(file == null))
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"არასწორი ფოტოს ტიპი!";
        }
    }
}
