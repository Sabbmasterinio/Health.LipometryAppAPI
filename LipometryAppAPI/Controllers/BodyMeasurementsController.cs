using AutoMapper;
using LipometryAppAPI.Contracts.Models;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Repositories;
using LipometryAppAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LipometryAppAPI.Controllers
{
    [ApiController]
    public class BodyMeasurementsController : Controller
    {
        #region Private Members
        private readonly IBodyMeasurementRepository _measurementRepository;
        private readonly IMapper _mapper;
        private readonly IBodyMeasurementService _measurementService;
        #endregion

        public BodyMeasurementsController(IBodyMeasurementRepository measurementRepository, IMapper mapper, IBodyMeasurementService measurementService)
        {
            _measurementRepository = measurementRepository;
            _mapper = mapper;
            _measurementService = measurementService;
        }

        [HttpGet(ApiEndpoints.Person.GetBodyMeasurementHistory)]
        [ProducesResponseType(typeof(List<BodyMeasurementReadResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBodyHistory([FromRoute] Guid id, CancellationToken token)
        {
            var personHistory = await _measurementRepository.GetHistoryByPersonIdAsync(id, token);
            if (personHistory is null)
                return NotFound();

            var result = _mapper.Map<List<PersonReadResponse>>(personHistory);
            return Ok(result);
        }

        [HttpPost(ApiEndpoints.BodyMeasurements.Measurements)]
        [ProducesResponseType(typeof(BodyMeasurementReadResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] BodyMeasurementCreateRequest model, CancellationToken token)
        {
            var createdMeasurement = await _measurementService.LogMeasurementAsync(model, token);
            var result = _mapper.Map<BodyMeasurementReadResponse>(createdMeasurement);

            return CreatedAtAction(nameof(GetAllBodyHistory), new { id = createdMeasurement.PersonId }, result);
        }
    }
}
