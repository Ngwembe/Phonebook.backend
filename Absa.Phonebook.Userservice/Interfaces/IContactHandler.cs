using Absa.Phonebook.Contactservice.Models;

namespace Absa.Phonebook.Contactservice.Interfaces
{
    public interface IContactHandler
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO contact);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> GetAllContactsAsync(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Contact>> GetAllContactsByPersonIdAsync(int personId);
        Task<Contact> GetContactAsync(int contactId);
        Task<bool> RemoveContactAsync(int contactId);
        Task<Contact> UpdateContactAsync(int contactId, Contact contact);
    }
}