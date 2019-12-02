using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class City : Base<short>
    {
        public string Name { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}
