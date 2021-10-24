using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly PolyclinicContext _context;
        public MedicalRecordService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor1> FindDoctorById(int userId)
        {
            return await _context.Doctors1.Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();
        }

        public async Task<Patient1> FindPatientById(int id)
        {
            return await _context.Patients1.Include(x => x.ApplicationUser).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MedicalRecord> FindMedicalRecordById(int id)
        {
            return await _context.MedicalRecords.Include(x => x.Patient)
                         .Include(x => x.Office).ThenInclude(x => x.Doctor)
                         .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
       
        public async Task<List<MedicalRecord>> GetMedicalRecordsForPatient(int id, int userId, 
            QueryParameters queryParameters)
        {
            var doctor = await FindDoctorById(userId);
            // ovako dolje sa where najkraći kod
            IQueryable<MedicalRecord> records = _context.MedicalRecords
                                                .Include(x => x.Patient)
                                                .Include(x => x.Office).ThenInclude(x => x.Doctor)
                        .Where(x => x.Patient1Id == id && x.Office.Doctor.ApplicationUserId == userId)
                                                .AsQueryable().OrderBy(x => x.Created);
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Office.Street.Contains(queryParameters.Query));
            }

            records = records.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateAsc":
                        records = records.OrderBy(p => p.Created);
                        break;
                    default:
                        records = records.OrderByDescending(n => n.Created);
                        break;
                }
            }    
            
            return await records.ToListAsync();        
        }

        public async Task<int> GetCountForMedicalRecordsForPatient(int id, int userId)
        {
            var doctor = await FindDoctorById(userId);

            var offices = await _context.Offices.Where(x => x.Doctor1Id == doctor.Id).ToListAsync();
           
            IEnumerable<int> ids = offices.Select(x => x.Id).ToList();

            return await _context.MedicalRecords.Where(x => x.Patient1Id == id && ids.Contains(x.Office1Id))
                         .CountAsync();
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsForAllDoctorPatients(int userId, 
            QueryParameters queryParameters)
        {       
            IQueryable<MedicalRecord> records = _context.MedicalRecords
                                                .Include(x => x.Patient)
                                                .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                .Where(x => x.Office.Doctor.ApplicationUserId == userId)
                                                .AsQueryable().OrderByDescending(x => x.Created);

            var office = await _context.Offices.Include(x => x.MedicalRecords).Include(x => x.Doctor)
                          .Where(x => x.Doctor.ApplicationUserId == userId && x.Id == queryParameters.OfficeId) 
                          .FirstOrDefaultAsync();
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Patient.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.OfficeId.HasValue)
            {
                records = records.Where(x => x.Office1Id == office.Id);

            }

            records = records.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateAsc":
                        records = records.OrderBy(p => p.Created);
                        break;
                    default:
                        records = records.OrderByDescending(n => n.Created);
                        break;
                }
            }    
            return await records.ToListAsync();        
        }

        public async Task<int> GetCountForMedicalRecordsForAllDoctorPatients(int userId)
        {
            return await _context.MedicalRecords.Include(x => x.Office).ThenInclude(x => x.Doctor) 
                         .Where(x => x.Office.Doctor.ApplicationUserId == userId)
                         .CountAsync();
        }

        public async Task<List<Office1>> GetOfficesForDoctor(int userId)
        {            
            return await _context.Offices.Include(x => x.Doctor) 
                         .Where(x => x.Doctor.ApplicationUserId == userId).ToListAsync();
        }

        public async Task<List<MedicalRecord>> GetMedicalRecordsForAllPatientDoctors(int userId, 
            QueryParameters queryParameters)
        {       
            IQueryable<MedicalRecord> records = _context.MedicalRecords
                                                .Include(x => x.Patient)
                                                .Include(x => x.Office).ThenInclude(x => x.Doctor)
                                                .Where(x => x.Patient.ApplicationUserId == userId)
                                                .AsQueryable().OrderByDescending(x => x.Created);

            var office = await _context.Offices.Where(x => x.Id == queryParameters.OfficeId) 
                         .FirstOrDefaultAsync();
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Office.Doctor.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.OfficeId.HasValue)
            {
                records = records.Where(x => x.Office1Id == office.Id);
            }

            records = records.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "dateAsc":
                        records = records.OrderBy(p => p.Created);
                        break;
                    default:
                        records = records.OrderByDescending(n => n.Created);
                        break;
                }
            }    
            return await records.ToListAsync();        
        }

        public async Task<int> GetCountForMedicalRecordsForAllPatientDoctors(int userId)
        {
            return await _context.MedicalRecords.Include(x => x.Patient)
                         .Where(x => x.Patient.ApplicationUserId == userId)
                         .CountAsync();
        }

        public async Task<List<Office1>> GetAllOffices()
        {            
            return await _context.Offices.ToListAsync();
        }



    
    }
}










