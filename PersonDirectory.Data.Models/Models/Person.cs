using PersonDirectory.Shared;
using PersonDirectory.Shared.Helper_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public GenderEnum GenderId { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string PersonalNumber { get; set; }

        [Required]
        [Date(18, 2)]
        public DateTime Birthdate { get; set; }

        public short CityId { get; set; }

        public ICollection<RelatedPerson> RelatedPeople { get; set; }
        public Gender Gender { get; set; }
        public City City { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }

    }
}
