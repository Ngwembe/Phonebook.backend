using System.ComponentModel.DataAnnotations;

namespace Absa.Phonebook.Contactservice.Models
{
    /// <summary>
    /// A Person with Id, FirstName and LastName fields
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The id of the contact
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// The FirstName of the contact
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the contact
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string LastName { get; set; }
    }
}
