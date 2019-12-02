using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class PhoneNumberType : Base<PhoneNumberTypeEnum>
    {
        public string Name { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
