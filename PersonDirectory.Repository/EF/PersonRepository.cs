using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data;
using PersonDirectory.Data.Models;
using PersonDirectory.Repository.Contracts.Base;
using PersonDirectory.Repository.EF.Base;
using PersonDirectory.Shared;

namespace PersonDirectory.Repository.EF
{
    public class PersonRepository : BaseRepository<Person, PersonDirectory.Service.Models.Person>, IPersonRepository
    {
        public PersonRepository(PersonDirectoryContext Context) : base(Context)
        {
        }

        public Person GetPersonById(int id)
        {
            return Context.People.Include(i => i.RelatedPeople).Include(i => i.PhoneNumbers).Include(i => i.City).FirstOrDefault(f => f.Id == id);
        }

        public List<Person> GetPeople(string firstName, string lastName, string personalNumber, GenderEnum? gender = null, string phoneNumber = null, DateTime? birthDate = null, int? currentPage = null, int? itemsPerPage = null)
        {
            List<Person> result = null;
            IQueryable<Person> query = Context.People.Include(p => p.PhoneNumbers).AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(p => p.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(p => p.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(personalNumber))
                query = query.Where(p => p.PersonalNumber.Contains(personalNumber));

            if (gender != null)
                query = query.Where(p => p.GenderId == gender);

            if (birthDate != null)
            {
                query = query.Where(p => p.Birthdate == birthDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                query = query.Where(p => p.PhoneNumbers.Any(ph => ph.Number.Contains(phoneNumber)));

            if (itemsPerPage != null && itemsPerPage > 0 && currentPage != null && currentPage > 0)
            {
                var skipCount = (currentPage - 1) * itemsPerPage;
                result = query.Skip(skipCount.Value).Take(itemsPerPage.Value).ToList();
            }
            else
                result = query.ToList();

            return result;
        }

        public List<PhoneNumber> GetPersonPhoneNumber(int personId)
        {
            var numbers = Context.PhoneNumbers.Where(pn => pn.PersonId == personId).ToList();
            return numbers;
        }

        public void RemovePersonPhoneNumber(PhoneNumber number)
        {
            Context.PhoneNumbers.Remove(number);
        }

        public void RemoveRelatedPerson(RelatedPerson relatedPerson)
        {
            Context.RelatedPeople.Remove(relatedPerson);
        }
        public void RemoveRelatedPerson(RelationTypeEnum type, int personId, int relatedPersonId)
        {
            var itemToDelete = Context.RelatedPeople.FirstOrDefault(r => r.RelationType == type && r.PersonId == personId && r.RelativePersonId == relatedPersonId);
            if (itemToDelete == null)
                throw new Exception("მონაცემები არ მოიძებნა");

            Context.RelatedPeople.Remove(itemToDelete);
        }

        public void AddRelatedPerson(RelatedPerson relatedPerson)
        {
            Context.RelatedPeople.Add(relatedPerson);
        }

        public List<Service.Models.RelatedPersonsReportItem> GetRelatedPersonsReport(int personId)
        {
            var report = Context.People.Where(p => p.Id == personId).SelectMany(p => p.RelatedPeople).GroupBy(p => p.RelationType)
                .Select(p => new Service.Models.RelatedPersonsReportItem
                {
                    RelationType = p.Key,
                    Count = p.Count(),
                    PersonId = personId
                }).ToList();

            return report;
        }
    }
}
