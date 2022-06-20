using System.ComponentModel.DataAnnotations;

namespace Absa.Phonebook.Contactservice.Models
{
    
    public class ContactDTO
    {
        /// <summary>
        /// The id of the contact
        /// </summary>
        public int ContactId { get; set; }

        /// <summary>
        /// The PhoneNumber of the contact
        /// </summary>
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The Foreign Key to the contact owner
        /// </summary>
        public int PersonId { get; set; }

        public PersonDTO Person { get; set; }
    }
}
