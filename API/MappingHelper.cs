using AutoMapper;
using Core.Entities;
using Core.Dtos;

namespace API
{
    public class MappingHelper : Profile
    {
        public MappingHelper()
        {
            CreateMap<Specialty, SpecialtyDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<Appointment, AppointmentToReturnDto>()
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.ApplicationUser.LastName))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.ApplicationUser.LastName));


            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<SubmissionFormDto, SubmissionForm>().ReverseMap();
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterDoctorDto, ApplicationUser>().ReverseMap();


        }
    }
}








