using Absa.Phonebook.Contactservice.Models;

namespace Absa.Phonebook.Contactservice.Interfaces
{
    public interface IPersonHandler
    {
        Task<PersonDTO> CreatePersonAsync(PersonDTO person);
        Task<IEnumerable<PersonDTO>> GetAllPersonsAsync();
        Task<IEnumerable<Person>> GetPersonAsync(int personId);
        Task<bool> RemovePersonAsync(int personId);
        Task<PersonDTO> UpdatePersonAsync(int personId, PersonDTO person);
    }
}