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

        public AthleteController(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Athlete>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var athletes = await _athleteRepository.GetAllAsync();
            return Ok(athletes);
        }


        /// <summary>
        /// Get athlete by Id
        /// </summary>
        /// <param name="id">The id</param>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Athlete), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var athlete = await _athleteRepository.GetByIdAsync(id);
            return athlete is null ? NotFound() : Ok(athlete);
        }

        /// <summary>
        /// Create a new athlete
        /// </summary>
        /// <param name="athlete">The athletes</param>
        [HttpPost]
        [ProducesResponseType(typeof(Athlete), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Athlete athlete)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAthlete = await _athleteRepository.AddAsync(athlete);
            return CreatedAtAction(nameof(Get), new { id = createdAthlete.PersonId }, createdAthlete);
        }

        /// <summary>
        /// Update an existing athlete
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="athlete">The athletes</param>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Athlete), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Athlete athlete)
        {
            var existingAthlete = await _athleteRepository.GetByIdAsync(id);
            if (existingAthlete is null)
                return NotFound();

            existingAthlete.FirstName = athlete.FirstName;
            existingAthlete.LastName = athlete.LastName;
            existingAthlete.HeightInCm = athlete.HeightInCm;
            existingAthlete.WeightInKg = athlete.WeightInKg;
            existingAthlete.WaistInCm = athlete.WaistInCm;
            existingAthlete.HipInCm = athlete.HipInCm;
            existingAthlete.NeckInCm = athlete.NeckInCm;
            existingAthlete.Sport = athlete.Sport;

            await _athleteRepository.UpdateAsync(existingAthlete);
            return Ok(existingAthlete);
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
    }
}
