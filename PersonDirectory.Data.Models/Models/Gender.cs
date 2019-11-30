using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class Gender : Base<GenderEnum>
    {
        public string Name { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
