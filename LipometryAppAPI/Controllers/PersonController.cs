using AutoMapper;
using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        #region Private Members
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        #endregion

        #region Constructors
        public PersonController(IPersonRepository personRepository, IMapper mapper, IPersonService personService)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _personService = personService;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Get all people
        /// </summary>
        [HttpGet(ApiEndpoints.Person.BasePerson)]
        [ProducesResponseType(typeof(List<PersonReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] AgeGroup? ageGroup, CancellationToken token)
        {
            if (ageGroup is not null)
                return Ok(await _personRepository.GetByAgeGroupAsync(ageGroup.Value));

            var people = await _personRepository.GetAllAsync(token);
            var result = _mapper.Map<List<PersonReadResponse>>(people);
            return Ok(result);
        }

        /// <summary>
        /// Get people in a paged format
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Person.GetPaged)]
        [ProducesResponseType(typeof(PagedResult<PersonReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] PaginationParameters pagination,
            [FromQuery] PersonGender? gender,
            CancellationToken token)
        {
            var result = await _personRepository.GetPagedAsync(
                pagination.Page,
                pagination.PageSize,
                gender.HasValue? p => p.PersonGender == gender.Value: null,
                token
            );

            return Ok(result);
        }

        /// <summary>
        /// Get person by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet(ApiEndpoints.Person.GetById)]
        [ProducesResponseType(typeof(PersonReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var person = await _personRepository.GetByIdAsync(id, token);
            if (person is null)
                return NotFound();

            var result = _mapper.Map<PersonReadResponse>(person);
            return Ok(result);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="person">The people</param>
        //[Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPost(ApiEndpoints.Person.Create)]
        [ProducesResponseType(typeof(PersonReadResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] PersonCreateRequest model, CancellationToken token)
        {
            var createdPerson = await _personService.CreateAsync(model, token);
            var result = _mapper.Map<PersonReadResponse>(createdPerson);

            return CreatedAtAction(nameof(Get), new { id = createdPerson.PersonId }, result);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The person</param>
        //[Authorize(AuthConstants.TrustedMemberPolicyName)]
        [HttpPut(ApiEndpoints.Person.Update)]
        [ProducesResponseType(typeof(PersonReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PersonUpdateRequest model, CancellationToken token)
        {
            var existing = await _personService.UpdateAsync(id, model, token);

            if (existing is null)
                return NotFound();

            var result = _mapper.Map<PersonReadResponse>(existing);

            return Ok(result);
        }

        /// <summary>
        /// Remove a person by id
        /// </summary>
        /// <param name="id">The id</param>
        //[Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.Person.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] Guid id, CancellationToken token)
        {
            var isRemoved = await _personService.RemoveAsync(id, token);

            if (isRemoved)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Get people by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Person.GetByGender)]
        [ProducesResponseType(typeof(List<PersonReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGender(PersonGender gender, CancellationToken token)
        {
            var people = await _personRepository.GetByGenderAsync(gender, token);
            var result = _mapper.Map<List<PersonReadResponse>>(people);
            return Ok(result);
        }

        /// <summary>
        /// Get all adults (18+)
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Person.GetAdults)]
        [ProducesResponseType(typeof(List<PersonReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdults18Plus(CancellationToken token)
        {
            var people = await _personRepository.GetAdultsAsync(token);
            var result = _mapper.Map<List<PersonReadResponse>>(people);
            return Ok(result);
        }
        #endregion
    }
}
