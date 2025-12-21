using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LipometryAppAPI.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing athlete resources, including retrieval, creation, updating, and deletion of
    /// athlete data.
    /// </summary>
    /// <remarks>The <see cref="AthleteController"/> exposes RESTful endpoints for clients to interact with
    /// athlete records. All endpoints require valid input data and return appropriate HTTP status codes based on the
    /// operation outcome.</remarks>
    [ApiController]
    [Authorize]
    public class AthleteController : Controller
    {
        #region Private Members
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;
        private readonly IAthleteService _athleteService;
        #endregion

        #region Constructors
        public AthleteController(IAthleteRepository athleteRepository, IMapper mapper, IAthleteService athleteService)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
            _athleteService = athleteService;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet(ApiEndpoints.Athlete.GetAll)]
        [ProducesResponseType(typeof(List<AthleteReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var athletes = await _athleteRepository.GetAllAsync(token);
            var result = _mapper.Map<List<AthleteReadResponse>>(athletes);
            return Ok(result);
        }

        /// <summary>
        /// Get athlete by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet(ApiEndpoints.Athlete.GetById)]
        [ProducesResponseType(typeof(AthleteReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id, token);
            if (athlete is null)
                return NotFound();
            
            var result = _mapper.Map<AthleteReadResponse>(athlete);
            return Ok(result);
        }

        /// <summary>
        /// Create a new athlete
        /// </summary>
        /// <param name="model">The athlete</param>
        [HttpPost(ApiEndpoints.Athlete.Create)]
        [ProducesResponseType(typeof(AthleteReadResponse), StatusCodes.Status201Created)]
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        public async Task<IActionResult> Create([FromBody] AthleteCreateRequest model, CancellationToken token)
        {
            var createdAthlete = await _athleteService.CreateAsync(model, token);
            var result = _mapper.Map<AthleteReadResponse>(createdAthlete);
            
            return CreatedAtAction(nameof(Get), new { id = createdAthlete.PersonId }, createdAthlete);
        }

        /// <summary>
        /// Update an existing athlete
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The athlete</param>
        [HttpPut(ApiEndpoints.Athlete.Update)]
        [ProducesResponseType(typeof(AthleteReadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthConstants.TrustedMemberPolicyName)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AthleteUpdateRequest model, CancellationToken token)
        {
            var existing = await _athleteService.UpdateAsync(id, model, token);

            if (existing is null)
                return NotFound();

            var result = _mapper.Map<AthleteReadResponse>(existing);

            return Ok(result);
        }

        /// <summary>
        /// Remove an athlete by id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpDelete(ApiEndpoints.Athlete.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthConstants.AdminUserPolicyName)]
        public async Task<IActionResult> Remove([FromRoute] Guid id, CancellationToken token)
        {
            var isRemoved = await _athleteService.RemoveAsync(id, token);

            if (isRemoved)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Get athletes by sport
        /// </summary>
        /// <param name="sport"></param>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Athlete.GetBySport)]
        [ProducesResponseType(typeof(List<AthleteReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySport([FromRoute] string sport, CancellationToken token)
        {
            var athletes = await _athleteRepository.GetBySportAsync(sport, token);
            var result = _mapper.Map<List<AthleteReadResponse>>(athletes);
            return Ok(result);
        }
        #endregion
    }
}
