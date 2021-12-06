using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IMedicalRecordService
    {
       // Task CreateMedicalRecord(MedicalRecord medicalRecord);
        Task<Doctor1> FindDoctorById(int userId);
        Task<Patient1> FindPatientById(int id);
        Task<List<MedicalRecord1>> GetMedicalRecordsForPatient(int id, int userId, QueryParameters queryParameters);
        Task<int> GetCountForMedicalRecordsForPatient(int id, int userId);
        Task<MedicalRecord1> FindMedicalRecordById(int id);
        Task<List<MedicalRecord1>> GetMedicalRecordsForAllDoctorPatients(int userId, QueryParameters queryParameters);
        Task<int> GetCountForMedicalRecordsForAllDoctorPatients(int userId);
        Task<List<Office1>> GetOfficesForDoctor(int userId);
        Task<List<MedicalRecord1>> GetMedicalRecordsForAllPatientDoctors(int userId, QueryParameters queryParameters);
        Task<int> GetCountForMedicalRecordsForAllPatientDoctors(int userId);
        Task<List<Office1>> GetAllOffices();
        Task CreateMedicalRecord1(MedicalRecord1 medicalRecord);
        Task UpdateMedicalRecord(MedicalRecord1 record);




    }
}