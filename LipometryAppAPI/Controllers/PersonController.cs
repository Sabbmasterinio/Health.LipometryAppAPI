using AutoMapper;
using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LipometryAppAPI.Controllers
{
    /// <summary>
    /// Handles HTTP API requests for managing people, including retrieving, creating, updating, and deleting person
    /// records.
    /// </summary>
    /// <remarks>The <see cref="PersonController"/> provides RESTful endpoints for working with person
    /// entities. It supports operations such as retrieving all people, fetching a person by ID, filtering by gender or
    /// adulthood, as well as creating, updating, and deleting person records. All endpoints return data in a format
    /// suitable for client consumption, typically using DTOs for input and output. <para> This controller requires
    /// dependency injection of an <see cref="IPersonRepository"/> for data access and an <see cref="IMapper"/> for
    /// mapping between domain models and DTOs. </para> <para> All actions return appropriate HTTP status codes, such as
    /// 200 (OK), 201 (Created), or 404 (Not Found), depending on the outcome of the operation. </para></remarks>
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public PersonController(IPersonRepository personRepository, IMapper mapper, IPersonService personService)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _personService = personService;
        }

        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet(ApiEndpoints.Person.BasePerson)]
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
        [HttpGet(ApiEndpoints.Person.GetById)]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
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
        [HttpPost(ApiEndpoints.Person.Create)]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PersonCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPerson = await _personService.CreateAsync(model);
            var result = _mapper.Map<PersonRead>(createdPerson);

            return CreatedAtAction(nameof(Get), new { id = createdPerson.PersonId }, result);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="person">The people</param>
        [HttpPut(ApiEndpoints.Person.Update)]
        [ProducesResponseType(typeof(PersonRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PersonUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _personService.UpdateAsync(id, model);

            var result = _mapper.Map<PersonRead>(existing);

            return Ok(result);
        }

        /// <summary>
        /// Remove a person by id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpDelete(ApiEndpoints.Person.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            await _personService.RemoveAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get people by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Person.GetByGender)]
        [ProducesResponseType(typeof(List<PersonRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGender(PersonGender gender)
        {
            var people = await _personRepository.GetByGenderAsync(gender);
            var result = _mapper.Map<List<PersonRead>>(people);
            return Ok(result);
        }

        /// <summary>
        /// Get all adults (18+)
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Person.GetAdults)]
        [ProducesResponseType(typeof(List<PersonRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdults()
        {
            var people = await _personRepository.GetAdultsAsync();
            var result = _mapper.Map<List<PersonRead>>(people);
            return Ok(result);
        }
    }
}
