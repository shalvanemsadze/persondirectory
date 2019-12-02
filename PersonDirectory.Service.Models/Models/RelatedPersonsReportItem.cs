using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class RelatedPersonsReportItem
    {
        public RelationTypeEnum RelationType { get; set; }
        public int Count { get; set; }
        public int PersonId { get; set; }
    }
}
