using System;
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
    public class Patient1Service : IPatient1Service
    {
        private readonly PolyclinicContext _context;
        public Patient1Service(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorOfficeSpecializationDto>> GetDoctorsWithSearchingAndPaging(
                QueryParameters queryParameters)
        {
            IQueryable<DoctorOfficeSpecializationDto> doctors = (from d in _context.Doctors1
                                                                join o in _context.Offices
                                                                on d.Id equals o.Doctor1Id
                                                                join ds in _context.DoctorSpecializations
                                                                on d.Id equals ds.Doctor1Id
                                                                join s in _context.Specializations
                                                                on ds.Specialization1Id equals s.Id
                                                                join ss in _context.Subspecializations
                                                                on s.Id equals ss.Specialization1Id
                                                                select new DoctorOfficeSpecializationDto()
                                                                {
                                                                    Name = d.Name,
                                                                    Street = o.Street,
                                                                    City = o.City,
                                                                    Country = o.Country,
                                                                    InitialExaminationFee = o.InitialExaminationFee,
                                                                    FollowUpExaminationFee = o.FollowUpExaminationFee
                                                                }).AsQueryable().OrderBy(x => x.InitialExaminationFee);

            if (queryParameters.HasQuery())
            {
                doctors = doctors
                .Where(x => x.Name.Contains(queryParameters.Query)
                || x.City.Contains(queryParameters.Query));

            }

            doctors = doctors.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await doctors.ToListAsync();        
        }

        public async Task<int> GetCountForDoctors()
        {
            return await _context.Offices.CountAsync();
        }

        public async Task<List<Office1>> GetOfficesWithSearchingPagingSorting(QueryParameters queryParameters)
        {
                                 
            var doctorSpecialization = await _context.DoctorSpecializations.
                                       Where(x => x.Specialization1Id == queryParameters.SpecializationId) 
                                       .FirstOrDefaultAsync();

            IQueryable<Office1> office = _context.Offices.Include(x => x.Doctor)
                                         .ThenInclude(x => x.DoctorSpecializations).ThenInclude(x => x.Doctor)
                                         .AsQueryable().OrderBy(x => x.City);
            
            if (queryParameters.HasQuery())
            {
                office = office
                .Where(x => x.City.Contains(queryParameters.Query)
                || x.Doctor.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.SpecializationId.HasValue)
            {
                office = office.Where(x => x.Doctor1Id == doctorSpecialization.Doctor1Id);

            }

            office = office.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);

            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "priceAsc":
                        office = office.OrderBy(p => p.InitialExaminationFee);
                        break;
                    case "priceDesc":
                        office = office.OrderByDescending(p => p.InitialExaminationFee);
                        break;
                    case "priceAscFollowUp":
                        office = office.OrderBy(p => p.FollowUpExaminationFee);
                        break;
                    case "priceDescFollowUp":
                        office = office.OrderByDescending(p => p.FollowUpExaminationFee);
                        break;
                    default:
                        office = office.OrderBy(n => n.City);
                        break;
                }
            }           
            return await office.ToListAsync();        
        }

        public async Task<List<Specialization1>> GetSpecializationsAsync()
        {
            var doctorSpecializations = await _context.DoctorSpecializations.ToListAsync();
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Specialization1Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();
            return specializations;
        }

        public async Task<int> GetCountForOffices()
        {
            return await _context.Offices.CountAsync();
        }

        public async Task<List<Patient1>> GetAllDoctorPatients(int userId,
             QueryParameters queryParameters)
        {   
            /* var office = await _context.Offices.Include(x => x.Doctor)
                          .Where(x => x.Doctor.ApplicationUserId == userId && x.Id == queryParameters.OfficeId) 
                          .ToListAsync();

            IEnumerable<int> ids1 = office.Select(x => x.Id);
 */
            var medicalrecords = await _context.MedicalRecords1.Include(x => x.Appointment)
                .ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                            .Where(x => x.Appointment.Office.Doctor.ApplicationUserId == userId).ToListAsync();

            IEnumerable<int> ids = medicalrecords.Select(x => x.Appointment1Id);

            var appointments = await _context.Appointments1.Where(x => ids.Contains(x.Id)).ToListAsync();

            IEnumerable<int?> ids2 = appointments.Select(x => x.Patient1Id);

            IQueryable<Patient1> patients =  _context.Patients1.Include(x => x.ApplicationUser)
                            .Where(x => ids2.Contains(x.Id))
                            .AsQueryable().OrderBy(x => x.Name);

            if (queryParameters.HasQuery())
            {
                patients = patients
                .Where(x => x.Name.Contains(queryParameters.Query));
            }

           /*  if (queryParameters.OfficeId.HasValue)
            {
                patients = patients.Where(x => ids1.Contains(x.Id));
            } 
  */
             patients = patients.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "nameDesc":
                        patients = patients.OrderByDescending(p => p.Name);
                        break;
                    default:
                        patients = patients.OrderBy(n => n.Name);
                        break;
                }
            }     
            return await patients.ToListAsync();        
        }

        public async Task<int> GetCountForAllDoctorPatients(int userId)
        {
             var medicalrecords = await _context.MedicalRecords1.Include(x => x.Appointment)
                .ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                            .Where(x => x.Appointment.Office.Doctor.ApplicationUserId == userId).ToListAsync();

            IEnumerable<int> ids = medicalrecords.Select(x => x.Appointment1Id);

            var appointments = await _context.Appointments1.Where(x => ids.Contains(x.Id)).ToListAsync();

            IEnumerable<int?> ids2 = appointments.Select(x => x.Patient1Id);

            return await _context.Patients1.Where(x => ids2.Contains(x.Id)).CountAsync();
        }

        public async Task<List<Gender>> GetGenders()
        {
            return await _context.Genders.OrderBy(x => x.GenderType).ToListAsync();
        }



    }
}







