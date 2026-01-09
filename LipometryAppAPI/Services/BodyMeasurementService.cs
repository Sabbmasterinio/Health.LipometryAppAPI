using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Exceptions;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;

namespace LipometryAppAPI.Services
{
    public class BodyMeasurementService : IBodyMeasurementService
    {
        private readonly IBodyMeasurementRepository _measurementRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BodyMeasurementService(IBodyMeasurementRepository mRepo, IPersonRepository pRepo, IUnitOfWork uow, IMapper mapper)
        {
            _measurementRepo = mRepo;
            _personRepo = pRepo;
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<BodyMeasurement> LogMeasurementAsync(BodyMeasurementCreateRequest request, CancellationToken token = default)
        {
            // 1. Validate Person exists
            var person = await _personRepo.GetByIdAsync(request.PersonId, token);
            if (person == null) throw new NotFoundException("Person not found");

            // 2. Create the Measurement Entity
            var measurement = _mapper.Map<BodyMeasurement>(request);

            // 3. Update the Person's "Current" stats
            // This copies matching properties (Weight, Height, Waist, etc.) from request to person
            _mapper.Map(request, person);

            _personRepo.Update(person, token); // Mark person as modified
            await _measurementRepo.CreateAsync(measurement, token); // Add history record

            await _unitOfWork.SaveChangesAsync(token); // Transaction commits both

            return measurement;
        }
    }
}
