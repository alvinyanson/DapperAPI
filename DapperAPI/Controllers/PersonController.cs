using DapperData.Models;
using DapperData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }


        // GET: api/<PersonController> 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = await _personRepository.GetPeople();

            return Ok(people);
        }

        // GET api/<PersonController>/5 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personRepository.GetPersonById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            if(!ModelState.IsValid)
                return BadRequest(person);

            var result = await _personRepository.AddPerson(person);

            if(!result)
                return BadRequest(person);
            
            return Ok(person);

        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Person newPerson)
        {
            if (!ModelState.IsValid)
                return BadRequest(newPerson);

            var person = await _personRepository.GetPersonById(id);

            if(person == null)
                return NotFound();

            newPerson.Id = person.Id;

            var result = await _personRepository.UpdatePerson(newPerson);

            if (!result)
                return BadRequest(person);

            return Ok(person);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personRepository.GetPersonById(id);
            
            if(person == null)
                return NotFound();

            var result = await _personRepository.DeletePerson(id);

            if (!result)
                return BadRequest();

            return Ok($"Person deleted with id: {id}");
        }
    }
}
