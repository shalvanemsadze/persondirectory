using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.Service.Models;
using PersonDirectory.Service.BusinessLogic;
using PersonDirectory.Shared;
using System;
using System.ComponentModel;

namespace PersonDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly PersonService _service = null;
        public PersonController(ILogger<PersonController> logger, PersonService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]Person person)
        {
            var result = _service.Upsert(person, OperationTypeEnum.Add);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public ActionResult Edit([FromBody]Person person)
        {
            var result = _service.Upsert(person, OperationTypeEnum.Edit);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("relatedperson/add")]
        public ActionResult AddRelatedPerson([FromBody]RelatedPerson person)
        {
            _service.AddRelatedPerson(person);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="personId"></param>
        /// <param name="relatedPersonId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("relatedperson/delete/{type}/{personId}/{relatedPersonId}")]
        public ActionResult RemoveRelatedPerson(RelationTypeEnum type, int personId, int relatedPersonId)
        {
            _service.RemoveRelatedPerson(type, personId, relatedPersonId);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}")]
        public ActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalNumber"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public ActionResult Search(string firstName = null, string lastName = null, string personalNumber = null, int? currentPage = null, int? itemsPerPage = null)
        {
            var result = _service.GetPeople(firstName, lastName, personalNumber, currentPage: currentPage, itemsPerPage: itemsPerPage);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="personalNumber"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="birthDate"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("searchex")]
        public ActionResult SearchEx(string firstName = null, string lastName = null, string? personalNumber = null, GenderEnum? gender = null, string phoneNumber = null, DateTime? birthDate = null, int? currentPage = null, int? itemsPerPage = null)
        {
            var result = _service.GetPeople(firstName, lastName, personalNumber, gender, phoneNumber, birthDate, currentPage: currentPage, itemsPerPage: itemsPerPage);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("relatedpeoplereport/{personId}")]
        public ActionResult SearchEx(int personId)
        {
            var result = _service.GetRelatedPersonsReport(personId);
            return Ok(result);
        }
    }
}
