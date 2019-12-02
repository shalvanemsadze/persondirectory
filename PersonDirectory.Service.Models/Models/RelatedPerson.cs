using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class RelatedPerson : Base<uint>
    {
        public RelationTypeEnum RelationType { get; set; }

        public int PersonId { get; set; }

        public int RelativePersonId { get; set; }

        public Person Person { get; set; }
        public Person RelativePerson { get; set; }
    }
}
