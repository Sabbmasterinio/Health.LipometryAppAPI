using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;

namespace LipometryAppAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Person> CreateAsync(PersonCreate createPerson, CancellationToken token = default)
        {
            await _personRepository.CreateAsync(_mapper.Map<Person>(createPerson), token);
            await _unitOfWork.SaveChangesAsync(token);
            return _mapper.Map<Person>(createPerson);
        }

        public async Task<Person> UpdateAsync(Guid id, PersonUpdate updatePerson, CancellationToken token = default)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id, token) 
                ?? throw new Exception("Person not found");

            _mapper.Map(updatePerson, existingPerson);
            _personRepository.Update(existingPerson, token);

            await _unitOfWork.SaveChangesAsync(token);

            return existingPerson;
        }
        public async Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id, token)
                ?? throw new Exception("Person not found");

            await _personRepository.RemoveAsync(id, token);
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
