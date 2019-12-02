 
using PersonDirectory.Shared;
using PersonDirectory.Shared.Helper_Types.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonDirectory.Service.Models
{
    public class Person : Base<int>
    {
        [Required(ErrorMessage = "სახელი გადმოცემული არ არის")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "სახელი უნდა შედგებოდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოსგან")]
        [NameCheck]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "გვარი გადმოცემული არ არის")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "გვარი უნდა შედგებოდეს მინიმუმ 2 და მაქსიმუმ 50 სიმბოლოსგან")]
        [NameCheck]
        public string LastName { get; set; }

        public GenderEnum? Gender { get; set; }

        [Required(ErrorMessage = "პირადი ნომერი გადმოცემული არ არის")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან")]
        public string PersonalNumber { get; set; }

        [Required(ErrorMessage = "დაბადების თარიღი გადმოცემული არ არის")]
        [Date(120, 18, "დაბადების თარიღის მინიმალური მნიშვნელობა უნდა იყოს {0}, მაქსიმალური - {1}")]
        public DateTime Birthdate { get; set; }

        public string ImagePath { get; set; }

        public ICollection<RelatedPerson> RelatedPeople { get; set; }

        public City City { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }        
    }
}
