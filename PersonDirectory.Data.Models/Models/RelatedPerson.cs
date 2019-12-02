using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class RelatedPerson : Base<int>
    {
        public RelationTypeEnum RelationType { get; set; }

        public int PersonId { get; set; }
        public int RelativePersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Person RelativePerson { get; set; }
    }
}
