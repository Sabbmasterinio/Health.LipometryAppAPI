using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Exceptions;
using LipometryAppAPI.Models;
using LipometryAppAPI.Repositories;

namespace LipometryAppAPI.Services
{
    public class PersonService : IPersonService
    {
        #region Private Members
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Implemented Methods of IPersonService
        public async Task<Person> CreateAsync(PersonCreateRequest createPerson, CancellationToken token = default)
        {
            await _personRepository.CreateAsync(_mapper.Map<Person>(createPerson), token);
            await _unitOfWork.SaveChangesAsync(token);
            return _mapper.Map<Person>(createPerson);
        }

        public async Task<Person> UpdateAsync(Guid id, PersonUpdateRequest updatePerson, CancellationToken token = default)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id, token);

            if (existingPerson is null)
                return null!;

            _mapper.Map(updatePerson, existingPerson);
            _personRepository.Update(existingPerson, token);
            await _unitOfWork.SaveChangesAsync(token);
            return existingPerson;
        }
        public async Task<bool> RemoveAsync(Guid id, CancellationToken token = default)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id, token);

            if (existingPerson is null)
                return false;
            await _personRepository.RemoveAsync(id, token);
            await _unitOfWork.SaveChangesAsync(token);
            return true;
        }
        #endregion
    }
}
