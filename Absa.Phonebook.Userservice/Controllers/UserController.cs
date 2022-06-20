using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Absa.Phonebook.Contactservice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Absa.Phonebook.Contactservice.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly List<User> _users;

        public UserController()
        {
            _users = new List<User>()
            {
                new User { Id = 1, FirstName = "FirstName 1", LastName = "Lastname 1" },
                new User { Id = 2, FirstName = "FirstName 2", LastName = "Lastname 2" },
                new User { Id = 3, FirstName = "FirstName 3", LastName = "Lastname 3" },
                new User { Id = 4, FirstName = "FirstName 4", LastName = "Lastname 4" },
                new User { Id = 5, FirstName = "FirstName 5", LastName = "Lastname 5" }
            };
        }

        /// <summary>
        /// Retrieves names of all users.
        /// </summary>
        /// <returns>An ActionResult of collection type User</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(Ok(_users));
        }

        /// <summary>
        /// Retrieves name of a user using the specified user ID.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>An ActionResult of type User</returns>
        /// <response code="200">Success: Returns the requested user</response>
        /// <response code="400">BadRequest: id passed must be greater than 0 (zero)</response>
        /// <response code="404">NotFound: No user was found matching the specified id</response>
        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id <= 0)
                return await Task.FromResult(BadRequest());

            var user = _users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return await Task.FromResult(NotFound());

            return await Task.FromResult(Ok(user));

            //return await Task.FromResult(_users.First(x => x.Id == id));
        }

        /// <summary>
        /// Create a new user on the system.
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] UserDto user)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            _users.Add(new Models.User { Id = (_users.Count + 1), FirstName = user.FirstName, LastName = user.LastName });

            return Created("", user);
        }

        /// <summary>
        /// Updates an existing user record if found on the system using the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto user)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (_users.FirstOrDefault(x => x.Id == id) == null)
                return NotFound(user);

            var _user = new Models.User { FirstName = user.FirstName, LastName = user.LastName };

            _users.Remove(_user);

            return Ok();
        }

        /// <summary>
        /// Updates certain fields of an existing user record if found on the system using the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <remarks>
        /// Sample request (this request updates the user's first name) \
        /// PATCH /user/id \
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/firstname", \
        ///         "value": "new first name" \
        ///     } \
        /// ] 
        /// </remarks>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, JsonPatchDocument<UserDto> patchDocument)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            if (_users.FirstOrDefault(x => x.Id == id) == null)
                return NotFound(patchDocument);

            UserDto? modifierUser = null; // JsonConvert.DeserializeObject<UserDTO>(patchDocument);
            var _user = new User { FirstName = modifierUser?.FirstName, LastName = modifierUser?.LastName };

            _users.Remove(_user);

            return Ok();
        }


        /// <summary>
        /// Removes the user from the system. This action is permanent.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            User? user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound(user);

            _users.Remove(user);

            return Ok();
        }
    }
}
