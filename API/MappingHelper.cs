using AutoMapper;
using Core.Entities;
using Core.Dtos;
using NetTopologySuite.Geometries;
using System.Collections.Generic;

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

            CreateMap<RegisterDoctorDto1, ApplicationUser>().ReverseMap();
            CreateMap<Specialization1, SpecializationDto>().ReverseMap();
            CreateMap<Subspecialization1, SubspecializationDto>().ReverseMap();
            CreateMap<ProfessionalAssociation, ProfessionalAssociationDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            CreateMap<Publication1, PublicationDto>()
             .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.PublicationAuthorsTitleDate, o => o.MapFrom(s => s.PublicationAuthorsTitleDate));

            CreateMap<Office1, OfficeToReturnDto>()
            .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name))
            .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Location.Y))
            .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Location.X));

            CreateMap<OfficeDto, Office1>()
             .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<OfficeCreateDto, Office1>()
            .ForMember(x => x.Picture, options => options.Ignore())
             .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

          

            CreateMap<OfficeCreationDto, Office1>();

            CreateMap<Appointment1, Appointment1ToReturnDto>()
            .ForMember(d => d.OfficeAddress, o => o.MapFrom(s => s.Office.Street))
            .ForMember(d => d.Appointment1Id, o => o.MapFrom(s => s.MedicalRecord1.Appointment1Id))
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
                .ForMember(d => d.RecordId, o => o.MapFrom(s => s.MedicalRecord1.Appointment1Id))
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name))
                .ForMember(d => d.City, o => o.MapFrom(s => s.Office.City))
                .ForMember(d => d.Country, o => o.MapFrom(s => s.Office.Country))
                .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));

            CreateMap<Doctor1, Doctor1Dto>().ReverseMap();
                    
            CreateMap<Patient1, Patient1Dto>()
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.ApplicationUser.PhoneNumber))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.ApplicationUser.Email));

            CreateMap<HospitalAffiliation, HospitalAffiliationDto>()
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name));

           // CreateMap<MedicalRecordDto, MedicalRecord>().ReverseMap();

            CreateMap<MedicalRecordDto1, MedicalRecord1>().ReverseMap();
            
          /*   CreateMap<MedicalRecord, MedicalRecordToReturnDto>()
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Office.Doctor.Name))
                .ForMember(d => d.Office, o => o.MapFrom(s => s.Office.Street))
                .ForMember(d => d.Patient, o => o.MapFrom(s => s.Patient.Name)); */

            CreateMap<MedicalRecord1, MedicalRecordToReturnDto>()
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Appointment.Office.Doctor.Name))
                .ForMember(d => d.Office, o => o.MapFrom(s => s.Appointment.Office.Street))
                .ForMember(d => d.Patient, o => o.MapFrom(s => s.Appointment.Patient.Name));

            CreateMap<RegisterDoctorDto1, Doctor1>()
                .ForMember(x => x.Picture, options => options.Ignore())
                .ForMember(x => x.DoctorSpecializations2, options => options.MapFrom(MapDoctorSpecialization2));

            CreateMap<EditDoctor1Dto, Doctor1>()
                .ForMember(x => x.Picture, options => options.Ignore())
                .ForMember(x => x.DoctorSpecializations2, options => options.MapFrom(MapDoctorSpecialization3));

             CreateMap<Doctor1, Doctor1Dto>()
                .ForMember(d => d.Specializations, o => o.MapFrom(MapForSpecialization2));

                CreateMap<Photo, PhotoDto>();



            #endregion
        }

        private List<DoctorSpecialization2> MapDoctorSpecialization2(RegisterDoctorDto1 doctorDto, Doctor1 doctor)
        {
            var result = new List<DoctorSpecialization2>();

            if (doctorDto.SpecializationsIds == null) { return result; }

            foreach (var id in doctorDto.SpecializationsIds)
            {
                result.Add(new DoctorSpecialization2() { Specialization1Id = id });
            }

            return result;
        }

        private List<DoctorSpecialization2> MapDoctorSpecialization3(EditDoctor1Dto doctorDto, Doctor1 doctor)
        {
            var result = new List<DoctorSpecialization2>();

            if (doctorDto.SpecializationsIds == null) { return result; }

            foreach (var id in doctorDto.SpecializationsIds)
            {
                result.Add(new DoctorSpecialization2() { Specialization1Id = id });
            }

            return result;
        }
        
        // ovo ne šljaka za sada
        private List<SpecializationDto> MapForSpecialization2(Doctor1 doctor, Doctor1Dto doctorDto)
        {
            var result = new List<SpecializationDto>();

            if (doctor.DoctorSpecializations2 != null)
            {
                foreach (var specialization in doctor.DoctorSpecializations2)
                {
                    result.Add(new SpecializationDto() { Id = specialization.Specialization1Id, 
                    SpecializationName = specialization.Specialization.SpecializationName });
                }
            }

            return result;
        }

    }
}








