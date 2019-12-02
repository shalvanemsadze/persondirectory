using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PersonDirectory.Data;
using PersonDirectory.Data.Models;
using PersonDirectory.Repository.Contracts.Base;
using PersonDirectory.Repository.EF.Base;

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

        public List<Person> GetPeople(string firstName, string lastName, string personalNumber)
        {
            IQueryable<Person> query = Context.People.AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(p => p.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(p => p.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(personalNumber))
                query = query.Where(p => p.PersonalNumber.Contains(personalNumber));

            var result = query.ToList();
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
    }
}
