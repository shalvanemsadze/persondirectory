using PersonDirectory.Shared;
using PersonDirectory.Shared.Helper_Types.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Data.Models
{
    public class Person : Base<int>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        public GenderEnum? GenderId { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }

        [Required]
        [Date(120, 18)]
        public DateTime Birthdate { get; set; }

        public short? CityId { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<RelatedPerson> RelatedPeople { get; set; }
        public virtual ICollection<RelatedPerson> PeopleByRelated { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }

    }
}
