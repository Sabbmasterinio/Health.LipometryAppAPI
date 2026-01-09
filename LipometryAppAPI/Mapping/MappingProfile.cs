using AutoMapper;
using LipometryAppAPI.Contracts.Requests;
using LipometryAppAPI.Contracts.Responses;
using LipometryAppAPI.Models;

namespace LipometryAppAPI.Mapping
{
    public class MappingProfile : Profile
    {
        #region Constructors
        public MappingProfile()
        {
            CreateMap<Person, PersonReadResponse>();
            CreateMap<PersonCreateRequest, Person>();
            CreateMap<PersonUpdateRequest, Person>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore());

            CreateMap<Athlete, AthleteReadResponse>();
            CreateMap<AthleteCreateRequest, Athlete>();
            CreateMap<AthleteUpdateRequest, Athlete>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore());

            CreateMap<BodyMeasurement, BodyMeasurementReadResponse>();
            CreateMap<BodyMeasurementCreateRequest, BodyMeasurement>();

            CreateMap<BodyMeasurementCreateRequest, Person>()
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.Ignore())
                .ForMember(dest => dest.LastName, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth, opt => opt.Ignore());
        }
        #endregion
    }
}
