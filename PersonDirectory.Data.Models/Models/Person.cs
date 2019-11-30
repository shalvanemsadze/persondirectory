using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Data.Models
{
    public class Person : Base<uint>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        public GenderEnum GenderId { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }

        public int CityId { get; set; }

       // public ICollection<RelatedPerson> RelatedPeople { get; set; }
      // public Gender Gender { get; set; }
       // public City City { get; set; }
         
    }
}
