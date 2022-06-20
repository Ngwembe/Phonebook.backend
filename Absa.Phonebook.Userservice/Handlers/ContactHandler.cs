using Absa.Phonebook.Contactservice.Helpers;
using Absa.Phonebook.Contactservice.Interfaces;
using Absa.Phonebook.Contactservice.Models;

namespace Absa.Phonebook.Contactservice.Handlers
{
    public class ContactHandler : IContactHandler
    {
        private readonly IRepository _repository;

        public ContactHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await Task.FromResult(_repository.All<Contact>());
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await Task.FromResult(_repository.All<Contact>(pageNumber, pageSize, (c => c.Person)));
        }

        public async Task<IEnumerable<Contact>> GetAllContactsByPersonIdAsync(int personId)
        {
            return await _repository.SearchAsync<Contact>(contact => contact.PersonId == personId);
        }

        public async Task<Contact> GetContactAsync(int contactId)
        {
            return (await _repository.SearchAsync<Contact>(contact => contact.ContactId == contactId)).FirstOrDefault();
        }
        public async Task<ContactDTO> CreateContactAsync(ContactDTO contact)
        {
            Person person = GetPerson(contact).Result;

            var contactToSave = MapperHelper.Map<Contact, ContactDTO>(new Contact(), contact);
            contactToSave.Person = person;

            var createdContact = await _repository.AddEntityAsync<Contact>(contactToSave);

            return MapperHelper.Map<ContactDTO, Contact>(new ContactDTO(), createdContact);

            async Task<Person> GetPerson(ContactDTO contact)
            {
                if (contact.PersonId > 0)
                    return _repository.Find<Person>(contact.PersonId);

                person = MapperHelper.Map<Person, PersonDTO>(new Person(),
                        new PersonDTO() { FirstName = contact.Person.FirstName, LastName = contact.Person.LastName });

                return await _repository.AddEntityAsync<Person>(person);
            }
        }
        public async Task<Contact> UpdateContactAsync(int contactId, Contact contact)
        {
            var existingRecord = await GetContactAsync(contactId);

            if (existingRecord == null)
                throw new Exception($"No contact found with specified Id");

            var updatedContact = await _repository.UpdateEntityAsync<Contact>(contact);

            return updatedContact;
        }
        public async Task<bool> RemoveContactAsync(int contactId)
        {
            var existingRecord = await GetContactAsync(contactId);

            _repository.DeleteEntity<Contact>(existingRecord);

            //if (!existingRecord.Any())
            //    throw new Exception($"No contact found with specified Id");

            //_repository.DeleteEntity<Contact>(existingRecord.First());

            return true;
        }
    }
}
