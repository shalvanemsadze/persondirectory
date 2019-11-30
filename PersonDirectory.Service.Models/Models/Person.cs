using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class Person : Base<uint>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string PersonalNumber { get; set; }

        public string BigCity { get; set; }

        public string Snow { get; set; }

        public string Rain { get; set; }
    }
}
