using Absa.Phonebook.Contactservice.Helpers;
using Absa.Phonebook.Contactservice.Interfaces;
using Absa.Phonebook.Contactservice.Models;

namespace Absa.Phonebook.Contactservice.Handlers
{
    public class PersonHandler : IPersonHandler
    {
        private readonly IRepository _repository;

        public PersonHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
        {
            return await Task.FromResult(_repository.All<Person>().Select(p => MapperHelper.Map<PersonDTO, Person>(new PersonDTO(), p)));
        }

        //public async Task<IEnumerable<Contact>> GetAllContactsByPersonIdAsync(int personId)
        //{
        //    return await _repository.SearchAsync<Contact>(contact => contact.PersonId == personId);
        //}

        public async Task<IEnumerable<Person>> GetPersonAsync(int personId)
        {
            return (await _repository.SearchAsync<Person>(contact => contact.PersonId == personId));
            //return (await _repository.SearchAsync<Person>(contact => contact.PersonId == personId))
            //        .Select(p => MapperHelper.Map<PersonDTO, Person>(new PersonDTO(), p));
        }

        public async Task<PersonDTO> CreatePersonAsync(PersonDTO personDTO)
        {
            var contactToSave = MapperHelper.Map<Person, PersonDTO>(new Person(), personDTO);

            var createdPerson = await _repository.AddEntityAsync<Person>(contactToSave);

            return MapperHelper.Map<PersonDTO, Person>(personDTO, createdPerson);
        }

        public async Task<PersonDTO> UpdatePersonAsync(int personId, PersonDTO personDTO)
        {
            var existingRecord = await GetPersonAsync(personId);

            if (existingRecord == null)
                throw new Exception($"No person found with specified Id");

            var personToSave = MapperHelper.Map<Person, PersonDTO>(new Person(), personDTO);

            var updatedPerson = await _repository.UpdateEntityAsync<Person>(personToSave);

            return MapperHelper.Map<PersonDTO, Person>(personDTO, updatedPerson);
        }

        public async Task<bool> RemovePersonAsync(int contactId)
        {
            var existingRecord = await GetPersonAsync(contactId);

            if (!existingRecord.Any())
                throw new Exception($"No contact found with specified Id");

            _repository.DeleteEntity<Person>(existingRecord.First());

            return true;
        }
    }
}
