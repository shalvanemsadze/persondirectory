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
           return Context.People.Include(i => i.RelatedPeople).FirstOrDefault(f => f.Id == id);
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
