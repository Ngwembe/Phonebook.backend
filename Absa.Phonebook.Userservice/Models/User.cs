using System.ComponentModel.DataAnnotations;

namespace Absa.Phonebook.Contactservice.Models
{
    /// <summary>
    /// A User with Id, FirstName and LastName fields
    /// </summary>
    public class User
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The FirstName of the user
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the user
        /// </summary>
        [Required] 
        public string LastName { get; set; }
    }
}
