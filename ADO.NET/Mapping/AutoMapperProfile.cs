using ADO.NET.DTOs.Response;
using ADO.NET.Entities;
using AutoMapper;

namespace ADO.NET.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Patient, PatientResponseDto>()
                .ForMember(dest => dest.PatientId,
                opt => opt.MapFrom(src => src.PatientId))
                .ForMember(dest => dest.PatientName,
                opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
