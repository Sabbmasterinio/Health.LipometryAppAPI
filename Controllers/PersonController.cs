using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly LipometryContext _context;

        public PersonController(LipometryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.People.Include(p => p.BodyDetails).ToListAsync());
        }


        /// <summary>
        /// Get person by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var person = await _context.People
                .SingleOrDefaultAsync(p => p.PersonId == id);

            return person is null ? NotFound() : Ok(person);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="person">The people</param>
        [HttpPost]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = person.PersonId }, person);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="person">The people</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Person person)
        {
            var existingPerson = await _context.People.FindAsync(id);

            if (existingPerson is null)
                return NotFound();

            existingPerson.FirstName= person.FirstName;
            existingPerson.LastName= person.LastName;
            existingPerson.DateOfBirth= person.DateOfBirth;
            //existingPerson.BodyDetails = person.BodyDetails;

            await _context.SaveChangesAsync();
            return Ok(existingPerson);
        }

        /// <summary>
        /// Remove a person by id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var existingPerson = await _context.People.FindAsync(id);

            if (existingPerson is null)
                return NotFound();

            _context.People.Remove(existingPerson);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
