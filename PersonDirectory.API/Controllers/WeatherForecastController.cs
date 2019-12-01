﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonDirectory.Service.Models;
using PersonDirectory.Service;
using PersonDirectory.Shared.Helper_Types.Exceptions;

namespace PersonDirectory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly PersonService _service = null;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, PersonService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Route("add/{person}")]
        public ActionResult Add([FromBody]Person person)
        {
           
               
                _service.Upsert(person);
           
        
            return Ok(new Person { FirstName ="aaa", LastName = "bbbdislll"});
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return null;
        }
    }
}
