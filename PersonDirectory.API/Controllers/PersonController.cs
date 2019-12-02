using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.Service.Models;
using PersonDirectory.Service.BusinessLogic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using PersonDirectory.Shared;

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

        [HttpPost]
        [Route("add/{person}")]
        public ActionResult Add([FromBody]Person person)
        {
            var result = _service.Upsert(person, OperationTypeEnum.Add);
            return Ok(result);
        }

        [HttpPost]
        [Route("edit/{person}")]
        public ActionResult Edit([FromBody]Person person)
        {
            var result = _service.Upsert(person, OperationTypeEnum.Edit);
            return Ok(result);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("get/{id}")]
        public ActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("search/{firstName?}/{lastName?}/{personalNumber?}")]
        public ActionResult Search(string firstName = null, string lastName = null, string personalNumber = null)
        {
            var result = _service.GetPeople(firstName, lastName, personalNumber);
            return Ok(result);
        }
    }
}
