using PersonDirectory.Data;
using PersonDirectory.Service.Models;
using PersonDirectory.Shared.Helper_Types.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Service
{
    public class PersonService : BaseService
    {
        public PersonService(PersonDirectoryContext context) : base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public int Upsert(Person person)
        {
            CheckNull(person);
            ValidateModelState(person);

            return 0;
        }
    }
}
