using System.ComponentModel.DataAnnotations;

namespace Absa.Phonebook.Contactservice.Models
{
    /// <summary>
    /// A User with Id, FirstName and LastName fields
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The FirstName of the user
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the user
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
    }
}
