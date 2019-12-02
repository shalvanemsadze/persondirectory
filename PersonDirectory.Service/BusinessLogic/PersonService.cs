﻿using PersonDirectory.Data;
using PersonDirectory.Repository.Contracts.Base;
using PersonDirectory.Repository.EF;
using PersonDirectory.Service.BusinessLogic.Base;
using PersonDirectory.Service.Models;
using PersonDirectory.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PersonDirectory.Service.BusinessLogic
{
    public class PersonService : BaseService
    {
        private readonly IPersonRepository _repository;
        public PersonService(PersonDirectoryContext context) : base(context)
        {
            _repository = Uow.Repository<IPersonRepository, PersonRepository>();
        }

        /// <summary>
        /// ფიზიკური პირის დამატება/რედაქტირება
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>

        public int Upsert(Person person, OperationTypeEnum operationType)
        {
            CheckNull(person);
            ValidateModelState(person);

            int result = 0;
            Data.Models.Person added = null;
            if (operationType == OperationTypeEnum.Add)
            {
                _repository.Add(person, out Data.Models.Person addedItem);
                added = addedItem;
            }
            else
            {
                if (operationType == OperationTypeEnum.Edit)
                {
                    var targetItem = _repository.SingleEntity(person.Id);

                    if (targetItem == null)
                        throw new System.Exception("პიროვნება ვერ მოიძებნა");

                    targetItem.FirstName = person.FirstName;
                    targetItem.LastName = person.LastName;
                    targetItem.GenderId = person.Gender;
                    targetItem.PersonalNumber = person.PersonalNumber;
                    targetItem.Birthdate = person.Birthdate;
                    if (person.City != null && person.City.Id != default)
                        targetItem.CityId = person.City.Id;
                    person.City = null;
                    result = targetItem.Id;

                    if (person.PhoneNumbers != null && person.PhoneNumbers.Count > 0)
                    {
                        var personNumbers = _repository.GetPersonPhoneNumber(person.Id);
                        personNumbers.ForEach(_repository.RemovePersonPhoneNumber);

                        targetItem.PhoneNumbers = Mapper.Map(person.PhoneNumbers, new Collection<Data.Models.PhoneNumber>());
                    }

                    _repository.Update(targetItem);
                }
            }

            Uow.SaveChanges();

            result = result == default ? added?.Id ?? 0 : result;
            return result;
        }

        /// <summary>
        /// ფიზიკური პირის წაშლა
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var targetItem = _repository.GetPersonById(id);
            if (targetItem == null)
                throw new System.Exception("პიროვნება ვერ მოიძებნა");

            if (targetItem.RelatedPeople != null && targetItem.RelatedPeople.Count > 0)
            {
                foreach (var item in targetItem.RelatedPeople)
                {
                    _repository.RemoveRelatedPerson(item);
                }
            }

            _repository.Remove(id);
            Uow.SaveChanges();
            return id;
        }

        /// <summary>
        /// ფიზიკური პირის სრული მონაცემების მიღება იდენტიფიკატორით
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetById(int id)
        {
            var dbItem = _repository.GetPersonById(id);
            if (dbItem == null)
                throw new System.Exception("პიროვნება ვერ მოიძებნა");

            var targetItem = Mapper.Map(dbItem, new Person());

            return targetItem;
        }

        /// <summary>
        ///  პიროვნებების ძებნა
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalNumber"></param>
        /// <returns></returns>
        public List<Person> GetPeople(string firstName, string lastName, string personalNumber)
        {
            var items = _repository.GetPeople(firstName, lastName, personalNumber);
            var result = Mapper.Map(items, new List<Person>());
            return result;
        }
    }
}