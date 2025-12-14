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

        public async Task<Person> CreateAsync(PersonCreate createPerson)
        {
            await _personRepository.CreateAsync(_mapper.Map<Person>(createPerson));
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Person>(createPerson);
        }

        public async Task<Person> UpdateAsync(int id, PersonUpdate updatePerson)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id) 
                ?? throw new Exception("Person not found");

            _mapper.Map(updatePerson, existingPerson);
            _personRepository.Update(existingPerson);

            await _unitOfWork.SaveChangesAsync();

            return existingPerson;
        }
        public async Task RemoveAsync(int id)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id)
                ?? throw new Exception("Person not found");

            await _personRepository.RemoveAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
