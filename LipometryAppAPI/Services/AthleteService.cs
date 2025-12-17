using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;

namespace LipometryAppAPI.Services
{
    public class AthleteService : IAthleteService
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AthleteService(IAthleteRepository athleteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Athlete> CreateAsync(AthleteCreate createAthlete, CancellationToken token = default)
        {
            await _athleteRepository.CreateAsync(_mapper.Map<Athlete>(createAthlete), token);
            await _unitOfWork.SaveChangesAsync(token);
            return _mapper.Map<Athlete>(createAthlete);
        }

        public async Task<Athlete> UpdateAsync(Guid id, AthleteUpdate updateAthlete, CancellationToken token = default)
        {
            var existingAthlete = await _athleteRepository.GetByIdAsync(id, token) 
                ?? throw new Exception("Athlete not found");
            _mapper.Map(updateAthlete, existingAthlete);
            _athleteRepository.Update(existingAthlete, token);
            await _unitOfWork.SaveChangesAsync(token);
            return existingAthlete;
        }
        public async Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            var existingPerson = await _athleteRepository.GetByIdAsync(id, token)
                ?? throw new Exception("Athlete not found");

            await _athleteRepository.RemoveAsync(id, token);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
