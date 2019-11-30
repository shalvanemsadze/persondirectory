using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Shared.Helper_Types
{
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute(int minYears, int maxYears) : base(typeof(DateTime), DateTime.Now.AddYears(-minYears).ToShortDateString(), DateTime.Now.AddYears(maxYears).ToShortDateString()) { }
    }
}
