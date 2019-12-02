using PersonDirectory.Data.Models;
using PersonDirectory.Shared;
using System;
using System.Collections.Generic;

namespace PersonDirectory.Repository.Contracts.Base
{
    public interface IPersonRepository : IBaseRepository<Person, PersonDirectory.Service.Models.Person>
    {
        List<PhoneNumber> GetPersonPhoneNumber(int personId);
        void RemovePersonPhoneNumber(PhoneNumber number);
        void RemoveRelatedPerson(RelatedPerson relatedPerson);
        Person GetPersonById(int id);
        List<Person> GetPeople(string firstName, string lastName, string personalNumber, GenderEnum? gender = null, string phoneNumber = null, DateTime? birthDate = null, int? currentPage = null, int? itemsPerPage = null);
    }
}