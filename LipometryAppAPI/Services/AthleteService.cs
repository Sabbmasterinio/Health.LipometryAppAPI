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

        public async Task<Athlete> CreateAsync(AthleteCreate createAthlete)
        {
            await _athleteRepository.CreateAsync(_mapper.Map<Athlete>(createAthlete));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Athlete>(createAthlete);
        }

        public async Task<Athlete> UpdateAsync(int id, AthleteUpdate updateAthlete)
        {
            var existingAthlete = await _athleteRepository.GetByIdAsync(id) 
                ?? throw new Exception("Athlete not found");
            _mapper.Map(updateAthlete, existingAthlete);
            _athleteRepository.Update(existingAthlete);
            await _unitOfWork.SaveChangesAsync();
            return existingAthlete;
        }
    }
}
