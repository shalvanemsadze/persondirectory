using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class Gender : Base<byte>
    {
        public string Name { get; set; }
    }
}
