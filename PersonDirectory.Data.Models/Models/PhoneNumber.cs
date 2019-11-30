using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class PhoneNumber : Base<uint>
    {
        public PhoneNumberTypeEnum Type { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Number { get; set; }

        public uint? PersonId { get; set; }
    }
}
