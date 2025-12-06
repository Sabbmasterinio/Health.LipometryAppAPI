using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
            return Ok(people);
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
            var person = await _personRepository.GetByIdAsync(id);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPerson = await _personRepository.AddAsync(person);
            return CreatedAtAction(nameof(Get), new { id = createdPerson.PersonId }, createdPerson);
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
            var existingPerson = await _personRepository.GetByIdAsync(id);
            if (existingPerson is null)
                return NotFound();

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.HeightInCm = person.HeightInCm;
            existingPerson.WeightInKg = person.WeightInKg;
            existingPerson.WaistInCm = person.WaistInCm;
            existingPerson.HipInCm = person.HipInCm;
            existingPerson.NeckInCm = person.NeckInCm;

            await _personRepository.UpdateAsync(existingPerson);
            return NoContent();

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
            if (!await _personRepository.ExistsAsync(id))
                return NotFound();

            await _personRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
