using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Shared.Helper_Types.Attributes
{
    public class DateAttribute : RangeAttribute
    {
        private string _errorMessage = string.Empty;
        public DateAttribute(int minYears, int maxYears, string errorMessage = null) : base(typeof(DateTime), DateTime.Now.AddYears(-minYears).ToString("dd/MM/yyyy"), DateTime.Now.AddYears(-maxYears).ToString("dd/MM/yyyy"))
        {
            _errorMessage = errorMessage;
        }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrWhiteSpace(_errorMessage))
                return base.FormatErrorMessage(name);

            var minimum = string.Empty;
            var maximum = string.Empty;

            if (Minimum != null && Minimum is DateTime)
                minimum = ((DateTime)Minimum).ToString("dd/MM/yyyy");

            if (Maximum != null && Maximum is DateTime)
                maximum = ((DateTime)Maximum).ToString("dd/MM/yyyy");

            return string.Format(_errorMessage, minimum, maximum);
        }
    }
}
