using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCatalystUserSearchAPI.Context;
using HealthCatalystUserSearchAPI.Data;
using HealthCatalystUserSearchAPI.Models;

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
                return NotFound();
            }

            var theUser = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.MyAddress)
                .Include(u => u.MyInterests)
                    .ThenInclude(i => i.Interest)
                .FirstOrDefaultAsync();

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
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
                return NotFound();
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
                return NotFound();
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
                return NotFound();
            }

            var returnUserDto = _mapper.Map<List<UserDto>>(theUsers);

            return Ok(returnUserDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] Guid id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
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
    }
}