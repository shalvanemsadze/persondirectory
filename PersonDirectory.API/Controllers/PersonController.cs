using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.Service.Models;
using PersonDirectory.Service.BusinessLogic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
            var result = _service.Upsert(person);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            return Ok(result);
        }
    }
}
