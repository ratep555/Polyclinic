using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IDoctorService
    {
        Task CreateDoctor(int userId, RegisterDoctorDto registerDoctorDto, string firstname, string lastname);
        Task CreateDoctor1(int userId, string firstname, string lastname);
        Task<Doctor1> FindDoctorById(int id);
        Task<List<Specialization1>> GetAllSpecializationsForDoctor(int id);
        Task<List<Subspecialization1>> GetAllSubSpecializationsForDoctor(int id);
        Task<List<ProfessionalAssociation>> GetAllProfessionalAssociationsForDoctor(int id);
        Task<List<Publication1>> GetAllPublicationsForDoctor(int id);
        Task<List<Office1>> GetOfficesForDoctor(int id);
        Task<bool> ChechIfAny(int id);
        Task<double> AverageVote(int id);



    }
}