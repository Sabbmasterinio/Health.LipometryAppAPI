using AutoMapper;
using LipometryAppAPI.Contracts;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personRepository.GetAllAsync();
            var result = _mapper.Map<List<PersonRead>>(people);
            return Ok(result);
        }


        /// <summary>
        /// Get person by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person is null)
                return NotFound();

            var result = _mapper.Map<PersonRead>(person);
            return Ok(result);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="person">The people</param>
        [HttpPost]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PersonCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPerson = await _personRepository.AddAsync(_mapper.Map<Person>(model));
            var result = _mapper.Map<PersonRead>(createdPerson);

            return CreatedAtAction(nameof(Get), new { id = createdPerson.PersonId }, result);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="person">The people</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _personRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            _mapper.Map(model, existing);
            await _personRepository.UpdateAsync(existing);

            var result = _mapper.Map<PersonRead>(existing);

            return Ok(result);
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

        // GET by gender
        [HttpGet("gender/{gender}")]
        [ProducesResponseType(typeof(List<PersonRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGender(PersonGender gender)
        {
            var people = await _personRepository.GetByGenderAsync(gender);
            var result = _mapper.Map<List<PersonRead>>(people);
            return Ok(result);
        }

        // GET adults
        [HttpGet("adults")]
        [ProducesResponseType(typeof(List<PersonRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdults()
        {
            var people = await _personRepository.GetAdultsAsync();
            var result = _mapper.Map<List<PersonRead>>(people);
            return Ok(result);
        }
    }
}
