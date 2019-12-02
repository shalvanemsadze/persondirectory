using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class City : Base<short>
    {
        public string Name { get; set; }
    }
}
