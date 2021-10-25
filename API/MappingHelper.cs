using AutoMapper;
using Core.Entities;
using Core.Dtos;
using NetTopologySuite.Geometries;

namespace API
{
    public class MappingHelper : Profile
    {
        public MappingHelper(GeometryFactory geometryFactory)
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
            CreateMap<Specialization1, SpecializationDto>().ReverseMap();
            CreateMap<Subspecialization1, SubspecializationDto>().ReverseMap();
            CreateMap<ProfessionalAssociation, ProfessionalAssociationDto>().ReverseMap();
            CreateMap<Publication1, PublicationDto>().ReverseMap();

            CreateMap<Office1, OfficeToReturnDto>()
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y))
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X));

            CreateMap<OfficeDto, Office1>()
             .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

          

            CreateMap<OfficeCreationDto, Office1>();

            CreateMap<Appointment1, Appointment1ToReturnDto>()
            .ForMember(d => d.OfficeAddress, o => o.MapFrom(s => s.Office.Street))
            //.ForMember(d => d.OfficeId, o => o.MapFrom(s => s.Office.Street))
            .ForMember(d => d.City, o => o.MapFrom(s => s.Office.City))
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name))
            .ForMember(d => d.DoctorId, o => o.MapFrom(s => s.Office.Doctor1Id));


            CreateMap<Appointment1Dto, Appointment1>().ReverseMap();
            CreateMap<Appointment1, Appointment1Dto>().ReverseMap()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status
            ));


            CreateMap<Appointment1, AppointmentSingleDto>()
            .ForMember(d => d.Office, o => o.MapFrom(s => s.Office.Street))
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name))
            .ForMember(d => d.City, o => o.MapFrom(s => s.Office.City))
            .ForMember(d => d.Country, o => o.MapFrom(s => s.Office.Country))
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));

            CreateMap<Doctor1, Doctor1Dto>().ReverseMap();
            
            CreateMap<Patient1, Patient1Dto>()
            .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.ApplicationUser.PhoneNumber))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.ApplicationUser.Email));

            CreateMap<MedicalRecordDto, MedicalRecord>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordToReturnDto>()
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name))
            .ForMember(d => d.Office, o => o.MapFrom(s => s.Office.Street))
            .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name));

            #endregion


        }
    }
}








