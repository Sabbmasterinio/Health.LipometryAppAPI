using LipometryAppAPI.Data;
using LipometryAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AthleteController : Controller
    {
        private readonly LipometryContext _context;

        public AthleteController(LipometryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all athletes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Athlete>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Athlete.ToListAsync());
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
            var person = await _context.Athlete
                .SingleOrDefaultAsync(p => p.AthleteId == id);

            return person is null ? NotFound() : Ok(person);
        }

        /// <summary>
        /// Create a new athlete
        /// </summary>
        /// <param name="athlete">The athletes</param>
        [HttpPost]
        [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Athlete athlete)
        {
            await _context.Athlete.AddAsync(athlete);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = athlete.AthleteId }, athlete);
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
            var existingAthlete = await _context.Athlete.FindAsync(id);

            if (existingAthlete is null)
                return NotFound();

            existingAthlete.FirstName = athlete.FirstName;
            existingAthlete.LastName = athlete.LastName;
            existingAthlete.HeightInCm = athlete.HeightInCm;
            existingAthlete.WeightInKg = athlete.WeightInKg;
            existingAthlete.WaistInCm = athlete.WaistInCm;
            existingAthlete.HipInCm = athlete.HipInCm;
            existingAthlete.NeckInCm = athlete.NeckInCm;

            await _context.SaveChangesAsync();
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
            var existingAthlete = await _context.Athlete.FindAsync(id);

            if (existingAthlete is null)
                return NotFound();

            _context.Athlete.Remove(existingAthlete);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
