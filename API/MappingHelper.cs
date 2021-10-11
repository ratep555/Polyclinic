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
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name)); 

            CreateMap<ExaminationToCreateDto, Examination>().ReverseMap();

            CreateMap<Examination, ExaminationToReturnDto>()
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name)); 


            CreateMap<SubmissionFormDto, SubmissionForm>().ReverseMap();
            CreateMap<RegisterDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterDoctorDto, ApplicationUser>().ReverseMap();
            CreateMap<RegisterEmployeeDto, ApplicationUser>().ReverseMap();

            #region 
            CreateMap<RegisterDoctorDto1, ApplicationUser>().ReverseMap();
            CreateMap<OfficeDto, Office1>().ReverseMap();

            CreateMap<Office1, OfficeToReturnDto>()
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name));

            CreateMap<Appointment1, Appointment1ToReturnDto>()
            .ForMember(d => d.OfficeAddress, o => o.MapFrom(s => s.Office.Street))
            .ForMember(d => d.City, o => o.MapFrom(s => s.Office.City))
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name));


            CreateMap<Appointment1Dto, Appointment1>().ReverseMap();

            #endregion


        }
    }
}








