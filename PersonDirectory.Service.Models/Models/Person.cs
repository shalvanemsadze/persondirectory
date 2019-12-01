using PersonDirectory.Shared;
using PersonDirectory.Shared.Helper_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class Person : Base<uint>
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public GenderEnum Gender { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 2)]
        public string PersonalNumber { get; set; }
      
        [Required]
        [Date(18, 2)]
        public DateTime Birthdate { get; set; }

        public string ImagePath { get; set; }

        public ICollection<RelatedPerson> RelatedPeople { get; set; }
       
        public City City { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
