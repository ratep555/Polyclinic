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

      /*   public async Task CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            medicalRecord.Created = medicalRecord.Created.ToLocalTime();

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();
        } */
        
        public async Task CreateMedicalRecord1(MedicalRecord1 medicalRecord)
        {
           // medicalRecord.Created = medicalRecord.Created.ToLocalTime();

            _context.MedicalRecords1.Add(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicalRecord(MedicalRecord1 record)
        {
            record.Created = record.Created.ToLocalTime();

            _context.Entry(record).State = EntityState.Modified;        
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

        public async Task<MedicalRecord1> FindMedicalRecordById(int id)
        {
            return await _context.MedicalRecords1.Include(x => x.Appointment).ThenInclude(x => x.Patient)
                         .Include(x => x.Appointment).ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                         .Where(x => x.Appointment1Id == id).FirstOrDefaultAsync();
        }
       
        public async Task<List<MedicalRecord1>> GetMedicalRecordsForPatient(int id, int userId, 
            QueryParameters queryParameters)
        {
            var doctor = await FindDoctorById(userId);
            // ovako dolje sa where najkraći kod
            IQueryable<MedicalRecord1> records = _context.MedicalRecords1
            .Include(x => x.Appointment)
            .ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
            .Where(x => x.Appointment.Patient1Id == id && x.Appointment.Office.Doctor.ApplicationUserId == userId)
                                                .AsQueryable().OrderBy(x => x.Created);
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Appointment.Office.Street.Contains(queryParameters.Query));
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

            return await _context.MedicalRecords1.Include(x => x.Appointment).
            Where(x => x.Appointment.Patient1Id == id && ids.Contains(x.Appointment.Office1Id))
                         .CountAsync();
        }

        public async Task<List<MedicalRecord1>> GetMedicalRecordsForAllDoctorPatients(int userId, 
            QueryParameters queryParameters)
        {       
            IQueryable<MedicalRecord1> records = _context.MedicalRecords1
                .Include(x => x.Appointment).ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                .Include(x => x.Appointment).ThenInclude(x => x.Patient)
                .Where(x => x.Appointment.Office.Doctor.ApplicationUserId == userId)
                .AsQueryable().OrderByDescending(x => x.Created);

            var office = await _context.Offices.Include(x => x.Doctor)
                          .Where(x => x.Doctor.ApplicationUserId == userId && x.Id == queryParameters.OfficeId) 
                          .FirstOrDefaultAsync();
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Appointment.Patient.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.OfficeId.HasValue)
            {
                records = records.Where(x => x.Appointment.Office1Id == office.Id);
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
            return await _context.MedicalRecords1.Include(x => x.Appointment).ThenInclude(x => x.Office) 
                         .Where(x => x.Appointment.Office.Doctor.ApplicationUserId == userId)
                         .CountAsync();
        }

        public async Task<List<Office1>> GetOfficesForDoctor(int userId)
        {            
            return await _context.Offices.Include(x => x.Doctor) 
                         .Where(x => x.Doctor.ApplicationUserId == userId).ToListAsync();
        }

        public async Task<List<MedicalRecord1>> GetMedicalRecordsForAllPatientDoctors(int userId, 
            QueryParameters queryParameters)
        {       
            IQueryable<MedicalRecord1> records = _context.MedicalRecords1
                    .Include(x => x.Appointment).ThenInclude(x => x.Patient)
                    .Include(x => x.Appointment).ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                    .Where(x => x.Appointment.Patient.ApplicationUserId == userId)
                    .AsQueryable().OrderByDescending(x => x.Created);

            var office = await _context.Offices.Include(x => x.Doctor).Where(x => x.Id == queryParameters.OfficeId) 
                         .FirstOrDefaultAsync();
            
            if (queryParameters.HasQuery())
            {
                records = records
                .Where(x => x.Appointment.Office.Doctor.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.OfficeId.HasValue)
            {
                records = records.Where(x => x.Appointment.Office1Id == office.Id);
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
            return await _context.MedicalRecords1.Include(x => x.Appointment).ThenInclude(x => x.Patient)
                         .Where(x => x.Appointment.Patient.ApplicationUserId == userId)
                         .CountAsync();
        }

        public async Task<List<Office1>> GetAllOffices()
        {            
            return await _context.Offices.ToListAsync();
        }



    
    }
}










