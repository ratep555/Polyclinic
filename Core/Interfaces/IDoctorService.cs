using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(int userId, RegisterDoctorDto registerDoctorDto, string firstname, string lastname);
        Task CreateDoctor1(int userId, string firstname, string lastname, RegisterDoctorDto1 registerdto);
        Task CreateDoctor2(int userId, Doctor1 doctor, string lastname, string firstname);
        Task<Doctor1> FindDoctorById(int id);
        Task<List<Specialization1>> GetAllSpecializationsForDoctor(int id);
        Task<List<Subspecialization1>> GetAllSubSpecializationsForDoctor(int id);
        Task<List<ProfessionalAssociation>> GetAllProfessionalAssociationsForDoctor(int id);
        Task<List<Publication1>> GetAllPublicationsForDoctor(int id);
        Task<List<Office1>> GetOfficesForDoctor(int id);
        Task<List<Specialization1>> GetAllSpecializationsForDoctorByUserId(int id);
        Task<List<Subspecialization1>> GetAllSubSpecializationsForDoctorByUserId(int id);
        Task<List<ProfessionalAssociation>> GetAllProfessionalAssociationsForDoctorByUserId(int id);
        Task<List<Publication1>> GetAllPublicationsForDoctorByUserId(int id);
        Task<List<Office1>> GetOfficesForDoctorByUserId(int id);
        Task<bool> ChechIfAny(int id);
        Task<double> AverageVote(int id);
        Task<List<Doctor1Dto>> GetAllDoctors(QueryParameters queryParameters);
        Task<int> GetCountForAllDoctors();
        Task<List<Doctor1>> GetFelipesDoctors(QueryParameters queryParameters);
        Task<List<Specialization1>> GetAllSpecializations();
        Task<string> GetRoleName(int userId);
        Task<List<Doctor1>> ShowListOfAllDoctors();
        Task<Doctor1> FindDoctorByApplicationUserId(int userId);
        Task<Doctor1> FindDoctorByApplicationUserIdIncludingSpecialization(int userId);
        Task<List<Specialization1>> GetNonSelectedSpecializations(List<int> ids);
        Task Save();


    }
}