using Absa.Phonebook.Contactservice.Interfaces;
using Absa.Phonebook.Contactservice.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Absa.Phonebook.Contactservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactHandler _contactHandler;

        public ContactController(IContactHandler contactHandler)
        {
            _contactHandler = contactHandler;
        }

        // GET: api/<ContactController>
        //[HttpGet]
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IEnumerable<Contact>> Get(int pageNumber = 1, int pageSize = 10)
        {
            return await _contactHandler.GetAllContactsAsync(pageNumber, pageSize);
            //return await _contactHandler.GetAllContactsAsync();
        }

        // GET api/<ContactController>/5
        [HttpGet("{contactId}")]
        public async Task<Contact> Get(int contactId)
        {
            return await _contactHandler.GetContactAsync(contactId);
        }
        
        //[HttpGet("{personId}/{contactId}")]
        //public async Task<IEnumerable<Contact>> Get(int personId, int contactId)
        //{
        //    return await _contactHandler.GetAllContactsByPersonIdAsync(personId);
        //}

        //TODO:Change to use a DTO object instead after AutoMapper config 
        // POST api/<ContactController>

        [HttpPost]
        public async Task<ContactDTO> Post([FromBody] ContactDTO contact)
        {
            return await _contactHandler.CreateContactAsync(contact);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{contactId}")]
        public async Task<Contact> Put(int contactId, [FromBody] Contact contact)
        {
            return await _contactHandler.UpdateContactAsync(contactId, contact);
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{contactId}")]
        public async Task<bool> Delete(int contactId)
        {
            return await _contactHandler.RemoveContactAsync(contactId);
        }
    }
}
