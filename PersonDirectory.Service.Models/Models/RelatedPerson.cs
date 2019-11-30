using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class RelatedPerson : Base<uint>
    {
        public RelationTypeEnum RelationType { get; set; }

        public uint PersonId { get; set; }
    }
}
