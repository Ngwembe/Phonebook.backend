using Absa.Phonebook.Contactservice.Helpers;
using Absa.Phonebook.Contactservice.Interfaces;
using Absa.Phonebook.Contactservice.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Absa.Phonebook.Contactservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonHandler _personHandler;

        public PersonController(IPersonHandler personHandler)
        {
            _personHandler = personHandler;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<IEnumerable<PersonDTO>> Get()
        {
            return await _personHandler.GetAllPersonsAsync();
        }

        // GET api/<PersonController>/5
        [HttpGet("{personId}")]
        public async Task<IEnumerable<PersonDTO>> Get(int personId)
        {
            return (await _personHandler.GetPersonAsync(personId)).Select(p => MapperHelper.Map<PersonDTO, Person>(new PersonDTO(), p));
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<PersonDTO> Post([FromBody] PersonDTO personDTO)
        {
            return await _personHandler.CreatePersonAsync(personDTO);
        }

        // PUT api/<PersonController>/5
        [HttpPut("{personId}")]
        public async Task<PersonDTO> Put(int personId, [FromBody] PersonDTO personDTO)
        {
            return await _personHandler.UpdatePersonAsync(personId, personDTO);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{personId}")]
        public async Task<bool> Delete(int personId)
        {
            return await _personHandler.RemovePersonAsync(personId);
        }
    }
}
