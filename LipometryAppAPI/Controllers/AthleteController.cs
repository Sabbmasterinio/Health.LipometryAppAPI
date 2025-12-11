using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AthleteController : Controller
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public AthleteController(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet]
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
        [HttpGet("{id:int}")]
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
        [HttpPost]
        [ProducesResponseType(typeof(AthleteRead), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] AthleteCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAthlete = await _athleteRepository.AddAsync(_mapper.Map<Athlete>(model));
            return CreatedAtAction(nameof(Get), new { id = createdAthlete.PersonId }, createdAthlete);
        }

        /// <summary>
        /// Update an existing athlete
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="athlete">The athletes</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(AthleteRead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AthleteUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _athleteRepository.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            // Apply the incoming update model to the existing entity
            _mapper.Map(model, existing);

            await _athleteRepository.UpdateAsync(existing);

            // Convert updated entity back to output DTO
            var readDto = _mapper.Map<PersonRead>(existing);

            return Ok(readDto);
        }

        /// <summary>
        /// Remove an athlete by id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpDelete("{id:int}")]
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

        [HttpGet("bysport/{sport}")]
        [ProducesResponseType(typeof(List<AthleteRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySport([FromRoute] string sport)
        {
            var athletes = await _athleteRepository.GetBySportAsync(sport);
            var result = _mapper.Map<List<AthleteRead>>(athletes);
            return Ok(result);
        }
    }
}
