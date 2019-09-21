using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCatalystUserSearchAPI.Context;
using HealthCatalystUserSearchAPI.JsonHelpers;
using HealthCatalystUserSearchAPI.Models;
using Microsoft.AspNetCore.Http;

namespace HealthCatalystUserSearchAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all users in database with known information
        /// </summary>
        /// <returns>A JSON document of known users</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var theUsers = await _context.Users
                .Include(u => u.MyAddress)
                .Include(u => u.MyInterests)
                    .ThenInclude(i => i.Interest)
                .ToListAsync();

            var returnUserDtos = _mapper.Map<List<UserDto>>(theUsers);
    
            return Ok(returnUserDtos); 
        }

        // GET: api/Users/5
        /// <summary>
        /// Returns a specific user from database with known information
        /// </summary>
        /// <param name="id">The desired user's id</param>
        /// <returns>A JSON document of the user</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] Guid id)
        {
            if (id == Guid.Empty || !UsersExists(id))
            {
                return new JsonErrorResult(new { message = "The id parameter is invalid" }, HttpStatusCode.BadRequest);
            }

            var theUser = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.MyAddress)
                .Include(u => u.MyInterests)
                    .ThenInclude(i => i.Interest)
                .FirstOrDefaultAsync();

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return new JsonErrorResult(new {message = "No Users were found for the provided parameters"}, HttpStatusCode.NotFound);
            }

            var returnUserDto = _mapper.Map<UserDto>(theUser);

            return Ok(returnUserDto);
        }

        /// <summary>
        /// Search for Users by Interests
        /// </summary>
        /// <param name="interestname">The name of the Interest</param>
        /// <param name="interesttype">The type of the Interest</param>
        /// <returns>A JSON document of the users that follow that interest.</returns>
        [HttpGet("SearchByInterest")]
        public async Task<IActionResult> SearchByInterest(string interestname, string interesttype)
        {
            // Build Query
            var query = _context.Interests.AsQueryable();

            if (!string.IsNullOrEmpty(interestname))
            {
                query = query.Where(interest => interest.InterestName.Equals(interestname, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(interesttype))
            {
                query = query.Where(a => a.InterestType.Equals(interesttype, StringComparison.InvariantCultureIgnoreCase));
            }

            var interests = query.Select(a => a.Id).ToList();

            // If none found.
            if (!interests.Any())
            {
                return new JsonErrorResult(new { message = "No Interests were found for the provided parameters" }, HttpStatusCode.NotFound);
            }

            // Get the users.
            var theUsers = await _context.Users
                .Include(u => u.MyAddress)
                .Include(u => u.MyInterests)
                    .ThenInclude(i => i.Interest)
                .Where(u => u.MyInterests.Any(f => interests.Contains(f.InterestId))).ToListAsync();

            var returnUserDto = _mapper.Map<List<UserDto>>(theUsers);

            return Ok(returnUserDto);
        }

        /// <summary>
        /// Search for Users by Name.
        /// </summary>
        /// <param name="lastname">Required: The first name of the User</param>
        /// <param name="firstname">The last name of the User</param>
        /// <returns>A JSON document of the users that follow that interest.</returns>
        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName(string lastname, string firstname)
        {
            if (string.IsNullOrEmpty(lastname))
            {
                return new JsonErrorResult(new { message = "The lastname parameter is invalid" }, HttpStatusCode.BadRequest);
            }

            // Build query.
            var query = _context.Users
                .Include(u => u.MyAddress)
                .Include(u => u.MyInterests)
                .ThenInclude(i => i.Interest)
                .Where(u => u.LastName.Equals(lastname, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(firstname))
            {
                query = query.Where(u => u.FirstName.Equals(firstname, StringComparison.InvariantCultureIgnoreCase));
            }

            var theUsers = await query.ToListAsync();

            // If none found.
            if (!theUsers.Any())
            {
                return new JsonErrorResult(new { message = "No Users were found for the provided parameters" }, HttpStatusCode.NotFound);
            }

            var returnUserDto = _mapper.Map<List<UserDto>>(theUsers);

            return Ok(returnUserDto);
        }

        ///// <summary>
        ///// Update a User in the database.
        ///// </summary>
        ///// <param name="id">The User Id for the User to be updated.</param>
        ///// <param name="user">The user to update as a JSON document.</param>
        ///// <returns></returns>
        ////[HttpPut("{id}")]
        ////public async Task<IActionResult> PutUsers([FromRoute] Guid id, [FromBody] UserDto user)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return BadRequest(ModelState);
        ////    }

        ////    if (id == Guid.Empty || !UsersExists(id))
        ////    {
        ////        return new JsonErrorResult(new { message = "The id parameter is invalid" }, HttpStatusCode.BadRequest);
        ////    }

        ////    if (id != new Guid(user.Id))
        ////    {
        ////        return new JsonErrorResult(new { message = "The id parameter does not match the User Id." }, HttpStatusCode.BadRequest);
        ////    }




        ////    _context.Entry(user).State = EntityState.Modified;

        ////    try
        ////    {
        ////        await _context.SaveChangesAsync();
        ////    }
        ////    catch (DbUpdateConcurrencyException)
        ////    {
        ////        if (!UsersExists(id))
        ////        {
        ////            return NotFound();
        ////        }

        ////        throw;
        ////    }

        ////    return NoContent();
        ////}

        /// <summary>
        /// Add a user to the application.
        /// </summary>
        /// <param name="user">The user to add as a JSON document.</param>
        /// <returns>A status of the action.</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostUser([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddInterestsForPost(user);

            var theAddress = DetermineTheAddressForPost(user);

            var newUsers = _mapper.Map<Users>(user);
            newUsers.MyAddress = theAddress;
            _context.Users.Add(newUsers);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = user.Id}, user);
        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsers([FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return Ok(users);
        //}

        /// <summary>
        /// Determine if the user is in the database
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <returns>True, if the user exists. Otherwise, False.</returns>
        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        /// <summary>
        /// Determine the Address for a Post by seeing if it already exists in the database before adding.
        /// </summary>
        /// <param name="user">The User information being Posted.</param>
        /// <returns>The proper address.</returns>
        private Addresses DetermineTheAddressForPost(UserDto user)
        {
            var existingAddress = _context.Addresses
                .First(z => z.City.Equals(user.MyAddress.City, StringComparison.InvariantCultureIgnoreCase) &&
                            z.Country.Equals(user.MyAddress.Country, StringComparison.InvariantCultureIgnoreCase) &&
                            z.State.Equals(user.MyAddress.State, StringComparison.InvariantCultureIgnoreCase) &&
                            z.Street1.Equals(user.MyAddress.Street1, StringComparison.InvariantCultureIgnoreCase) &&
                            z.Street2.Equals(user.MyAddress.Street2, StringComparison.InvariantCultureIgnoreCase) &&
                            z.ZipCode.Equals(user.MyAddress.ZipCode, StringComparison.InvariantCultureIgnoreCase));

            Addresses theAddress;
            if (existingAddress == null)
            {
                theAddress = _mapper.Map<Addresses>(user.MyAddress);
                _context.Addresses.Add(theAddress);
            }
            else
            {
                theAddress = _mapper.Map<Addresses>(existingAddress);
            }

            return theAddress;
        }

        /// <summary>
        /// Add any Interests from a Post that do not already exist in the database.
        /// </summary>
        /// <param name="user">The User information being Posted.</param>
        private void AddInterestsForPost(UserDto user)
        {
            var newInterests = _mapper.Map<ICollection<Interests>>(user.MyInterests);
            foreach (var interest in newInterests)
            {
                var existingInterest = _context.Interests.First(f => f.InterestName.Equals(interest.InterestName) && f.InterestType.Equals(interest.InterestType));

                if (existingInterest == null)
                {
                    _context.Interests.Add(interest);
                    var newUserToInterest = new UserToInterest
                    {
                        InterestId = interest.Id,
                        UserId = new Guid(user.Id)
                    };
                    _context.UserToInterest.Add(newUserToInterest);
                }
                else
                {
                    var newUserToInterest = new UserToInterest
                    {
                        InterestId = existingInterest.Id,
                        UserId = new Guid(user.Id)
                    };
                    _context.UserToInterest.Add(newUserToInterest);
                }
            }
        }
    }
}