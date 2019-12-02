using PersonDirectory.Data;
using PersonDirectory.Repository.Contracts.Base;
using PersonDirectory.Repository.EF;
using PersonDirectory.Service.BusinessLogic.Base;
using PersonDirectory.Service.Models;
using PersonDirectory.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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
                var dbPerson = Mapper.Map(person, new Data.Models.Person());
                dbPerson.City = null;
                added = _repository.Add(dbPerson);
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
                    targetItem.City = null;
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
        public Person GetById(int id, string imageBasePath = null)
        {
            var dbItem = _repository.GetPersonById(id);
            if (dbItem == null)
                throw new System.Exception("პიროვნება ვერ მოიძებნა");

            var targetItem = Mapper.Map(dbItem, new Person());

            if (!string.IsNullOrWhiteSpace(targetItem.ImagePath) && !string.IsNullOrWhiteSpace(imageBasePath))
            {
                var fileInfo = new FileInfo(@$"{imageBasePath}\{targetItem.ImagePath}");
                byte[] data = new byte[fileInfo.Length];
                using (var fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                }

                //fileInfo.Delete();
                targetItem.ImageContent = Convert.ToBase64String(data);
            }

            return targetItem;
        }

        /// <summary>
        ///   პიროვნებების ძებნა
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalNumber"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        public List<Person> GetPeople(string firstName, string lastName, string personalNumber, GenderEnum? gender = null, string phoneNumber = null, DateTime? birthDate = null, int? currentPage = null, int? itemsPerPage = null)
        {
            var items = _repository.GetPeople(firstName, lastName, personalNumber, gender, phoneNumber, birthDate, currentPage: currentPage, itemsPerPage: itemsPerPage);
            var result = Mapper.Map(items, new List<Person>());
            return result;
        }

        /// <summary>
        /// რეპორტი თუ რამდენი დაკავშირებული პირი ჰყავს თითოეულ ფიზიკურ პირს, კავშირის ტიპის მიხედვით
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<Service.Models.RelatedPersonsReportItem> GetRelatedPersonsReport(int personId)
        {
            return _repository.GetRelatedPersonsReport(personId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relatedPerson"></param>
        public void AddRelatedPerson(RelatedPerson relatedPerson)
        {
            var itemToAdd = Mapper.Map(relatedPerson, new Data.Models.RelatedPerson());
            _repository.AddRelatedPerson(itemToAdd);
            Uow.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="personId"></param>
        /// <param name="relatedPersonId"></param>
        public void RemoveRelatedPerson(RelationTypeEnum type, int personId, int relatedPersonId)
        {
            _repository.RemoveRelatedPerson(type, personId, relatedPersonId);
            Uow.SaveChanges();
        }

        public void UpdateImagePath(int personId, string path)
        {
            var targetItem = _repository.SingleEntity(personId);

            if (targetItem == null)
                throw new Exception("პიროვნება ვერ მოიძებნა");

            targetItem.ImagePath = path;
            Uow.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Gender> GetGenders()
        {
            var dbItems = _repository.GetGenders();
            var result = Mapper.Map(dbItems, new List<Gender>());
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<City> GetCities()
        {
            var dbItems = _repository.GetCities();
            var result = dbItems.Select(s => new City { Id = s.Id, Name = s.Name }).ToList();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RelationType> GetRelationTypes()
        {
            var dbItems = _repository.GetRelationTypes();
            var result = Mapper.Map(dbItems, new List<RelationType>());
            return result;
        }

        public Tuple<string, string, string, string> GetImageSaveAttributes(int personId, string extension, string baseDirectoryPath)
        {
            var fileName = $"person_photo_{personId}{extension}";
            var directoryPath = @$"{baseDirectoryPath}\{personId}";
            var fullPath = @$"{directoryPath}\{fileName}";
            var pathForSave = @$"{personId}\{fileName}";
            return new Tuple<string, string, string, string>(fileName, directoryPath, fullPath, pathForSave);
        }
    }
}
