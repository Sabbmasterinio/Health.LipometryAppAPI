using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonRead>();
            CreateMap<PersonCreate, Person>();
            CreateMap<PersonUpdate, Person>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore());

            CreateMap<Athlete, AthleteRead>();
            CreateMap<AthleteCreate, Athlete>();
            CreateMap<AthleteUpdate, Athlete>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore());
        }
    }
}
