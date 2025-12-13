using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
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
    public class AthleteController : Controller
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;
        private readonly IAthleteService _athleteService;

        public AthleteController(IAthleteRepository athleteRepository, IMapper mapper, IAthleteService athleteService)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
            _athleteService = athleteService;
        }

        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet(ApiEndpoints.Athlete.GetAll)]
        [ProducesResponseType(typeof(List<AthleteRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var athletes = await _athleteRepository.GetAllAsync();
            var result = _mapper.Map<List<AthleteRead>>(athletes);
            return Ok(result);
        }

        /// <summary>
        /// Get athlete by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet(ApiEndpoints.Athlete.GetById)]
        [ProducesResponseType(typeof(AthleteRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);
            if (athlete is null)
                return NotFound();

            var result = _mapper.Map<PersonRead>(athlete);
            return Ok(result);
        }

        /// <summary>
        /// Create a new athlete
        /// </summary>
        /// <param name="athlete">The athletes</param>
        [HttpPost(ApiEndpoints.Athlete.Create)]
        [ProducesResponseType(typeof(AthleteRead), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] AthleteCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAthlete = await _athleteService.CreateAsync(model);
            var result = _mapper.Map<PersonRead>(createdAthlete);
            
            return CreatedAtAction(nameof(Get), new { id = createdAthlete.PersonId }, createdAthlete);
        }

        /// <summary>
        /// Update an existing athlete
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="athlete">The athletes</param>
        [HttpPut(ApiEndpoints.Athlete.Update)]
        [ProducesResponseType(typeof(AthleteRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AthleteUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _athleteService.UpdateAsync(id, model);

            var result = _mapper.Map<PersonRead>(existing);

            return Ok(result);
        }

        /// <summary>
        /// Remove an athlete by id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpDelete(ApiEndpoints.Athlete.Remove)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var existingAthlete = await _athleteRepository.GetByIdAsync(id);

            if (existingAthlete is null)
                return NotFound();

            await _athleteRepository.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get athletes by sport
        /// </summary>
        /// <param name="sport"></param>
        /// <returns></returns>
        [HttpGet(ApiEndpoints.Athlete.GetBySport)]
        [ProducesResponseType(typeof(List<AthleteRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySport([FromRoute] string sport)
        {
            var athletes = await _athleteRepository.GetBySportAsync(sport);
            var result = _mapper.Map<List<AthleteRead>>(athletes);
            return Ok(result);
        }
    }
}
