using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Core.Paging;

namespace Infrastructure.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly PolyclinicContext _context;
        public DoctorService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task CreateDoctor(int userId, RegisterDoctorDto registerDoctorDto,
            string firstname, string lastname)
        {
            var doctor = new Doctor();
            doctor.ApplicationUserId = userId;
            doctor.Name = string.Format("{0} {1}", firstname, lastname);

            doctor.DepartmentId = registerDoctorDto.DepartmentId;
            doctor.SpecialtyId = registerDoctorDto.SpecialtyId;

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

        }
        public async Task CreateDoctor1(int userId, string lastname, 
            string firstname, RegisterDoctorDto1 registerdto)
        {
            var doctor = new Doctor1();
            doctor.ApplicationUserId = userId;
            doctor.Name = string.Format("{0} {1}", lastname, firstname);
            doctor.Resume = registerdto.Resume;
            doctor.StartedPracticing = registerdto.StartedPracticing;

            _context.Doctors1.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task CreateDoctor2(int userId, Doctor1 doctor, string lastname, 
            string firstname)
        {
            doctor.ApplicationUserId = userId;
            doctor.Name = string.Format("{0} {1}", lastname, firstname);

            _context.Doctors1.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor1> FindDoctorById(int id)
        {
            return await _context.Doctors1.Include(x => x.DoctorSpecializations2).ThenInclude(x => x.Specialization)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Doctor1> FindDoctorByApplicationUserId(int userId)
        {
            return await _context.Doctors1
            .Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Doctor1> FindDoctorByApplicationUserIdIncludingSpecialization(int userId)
        {
            return await _context.Doctors1.Include(x => x.DoctorSpecializations2).ThenInclude(x => x.Specialization)
                .Where(x => x.ApplicationUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<List<Specialization1>> GetAllSpecializationsForDoctor(int id)
        {

            var doctorSpecializations = await _context.DoctorSpecializations.
                                        Where(x => x.Doctor1Id == id)
                                       .ToListAsync();
            
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Specialization1Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();
            return specializations;
        }

        public async Task<List<Specialization1>> GetAllSpecializationsForDoctorByUserId(int userid)
        {
            var doctor = await FindDoctorByApplicationUserId(userid);

            var doctorSpecializations = await _context.DoctorSpecializations2.
                                        Where(x => x.Doctor1Id == doctor.Id)
                                       .ToListAsync();
            
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Specialization1Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();
            return specializations;
        }

        public async Task<List<Subspecialization1>> GetAllSubSpecializationsForDoctor(int id)
        {
            var doctorSpecializations = await _context.DoctorSpecializations.
                                        Where(x => x.Doctor1Id == id)
                                       .ToListAsync();
            
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Specialization1Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();

            IEnumerable<int> ids1 = specializations.Select(x => x.Id);

            var subSpecializations = await _context.Subspecializations.Include(x =>x.Specialization)
                                     .Where(x => ids1.Contains(x.Specialization1Id)).ToListAsync();

            return subSpecializations;
        }

        public async Task<List<Subspecialization1>> GetAllSubSpecializationsForDoctorByUserId(int userid)
        {
            var doctor = await FindDoctorByApplicationUserId(userid);

            var doctorSpecializations = await _context.DoctorSpecializations.
                                        Where(x => x.Doctor1Id == doctor.Id)
                                       .ToListAsync();
            
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Specialization1Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();

            IEnumerable<int> ids1 = specializations.Select(x => x.Id);

            var subSpecializations = await _context.Subspecializations.Include(x =>x.Specialization)
                                     .Where(x => ids1.Contains(x.Specialization1Id)).ToListAsync();

            return subSpecializations;
        }

        public async Task<List<ProfessionalAssociation>> GetAllProfessionalAssociationsForDoctor(int id)
        {

            var doctorProfessionalAssociations = await _context.DoctorProfessionalAssociations.
                                                 Where(x => x.Doctor1Id == id)
                                                .ToListAsync();
            
            IEnumerable<int> ids = doctorProfessionalAssociations.Select(x => x.ProfessionalAssociationId);

            var professionalAssociations = await _context.ProfessionalAssociations
                                           .Where(x => ids.Contains(x.Id)).ToListAsync();

            return professionalAssociations;
        }

        public async Task<List<ProfessionalAssociation>> GetAllProfessionalAssociationsForDoctorByUserId(int userid)
        {
            var doctor = await FindDoctorByApplicationUserId(userid);

            var doctorProfessionalAssociations = await _context.DoctorProfessionalAssociations.
                                                 Where(x => x.Doctor1Id == doctor.Id)
                                                .ToListAsync();
            
            IEnumerable<int> ids = doctorProfessionalAssociations.Select(x => x.ProfessionalAssociationId);

            var professionalAssociations = await _context.ProfessionalAssociations
                                           .Where(x => ids.Contains(x.Id)).ToListAsync();

            return professionalAssociations;
        }

        public async Task<List<Publication1>> GetAllPublicationsForDoctor(int id)
        {

            var doctorPublications = await _context.DoctorPublications.
                                    Where(x => x.Doctor1Id == id)
                                    .ToListAsync();
            
            IEnumerable<int> ids = doctorPublications.Select(x => x.Publication1Id);

            var publications = await _context.Publications
                                    .Where(x => ids.Contains(x.Id)).ToListAsync();

            return publications;
        }

        public async Task<List<Publication1>> GetAllPublicationsForDoctorByUserId(int userid)
        {
            var doctor = await FindDoctorByApplicationUserId(userid);

            var doctorPublications = await _context.DoctorPublications.
                                    Where(x => x.Doctor1Id == doctor.Id)
                                    .ToListAsync();
            
            IEnumerable<int> ids = doctorPublications.Select(x => x.Publication1Id);

            var publications = await _context.Publications
                                    .Where(x => ids.Contains(x.Id)).ToListAsync();

            return publications;
        }
        public async Task<List<Office1>> GetOfficesForDoctor(int id)
        {

            var doctor = await _context.Doctors1.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            var offices = await _context.Offices.Where(x => x.Doctor1Id == doctor.Id).ToListAsync();

            return offices;
        }

        public async Task<List<Office1>> GetOfficesForDoctorByUserId(int userid)
        {

            var doctor = await _context.Doctors1.Where(x => x.ApplicationUserId == userid).FirstOrDefaultAsync();
            
            var offices = await _context.Offices.Where(x => x.Doctor1Id == doctor.Id).ToListAsync();

            return offices;
        }

        public async Task<bool> ChechIfAny(int id)
        {
           return await _context.Ratings.AnyAsync(x => x.Doctor1Id == id);          
        }

        public async Task<double> AverageVote(int id)
        {
            var average = await _context.Ratings.Where(x => x.Doctor1Id == id).AverageAsync(x => x.Rate);

            return average;
        }

        public async Task<List<Doctor1Dto>> GetAllDoctors(QueryParameters queryParameters)
        {
                                 
            var doctorSpecialization = await _context.DoctorSpecializations.
                                       Where(x => x.Specialization1Id == queryParameters.SpecializationId) 
                                       .FirstOrDefaultAsync();

            IQueryable<Doctor1Dto> doctors = (from d in _context.Doctors1
                        select new Doctor1Dto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            StartedPracticing = d.StartedPracticing,
                            RateSum = _context.Ratings.Where(x => x.Doctor1Id == d.Id).Sum(x =>x.Rate),
                            Count = _context.Ratings.Where(x => x.Doctor1Id == d.Id).Count()
                           /*  AverageVote = 
                            (int?)_context.Ratings.Where(x => x.Doctor1Id == d.Id).Sum(x =>x.Rate) /
                            (int?)_context.Ratings.Where(x => x.Doctor1Id == d.Id).Count() */
                        }).AsQueryable().OrderBy(x => x.Name);
            
            if (queryParameters.HasQuery())
            {
                doctors = doctors
                .Where(x => x.Name.Contains(queryParameters.Query));
            }

            if (queryParameters.SpecializationId.HasValue)
            {
                doctors = doctors.Where(x => x.Id == doctorSpecialization.Doctor1Id);
            }

            doctors = doctors.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);

            var list = await doctors.ToListAsync();
            
            foreach (var item in list)
            {
                int? rating = item.RateSum;
                int? count = item.Count;

                if (item.RateSum.HasValue && item.RateSum > 0)
                {
                    item.AverageVote = await _context.Ratings.Where(x => x.Doctor1Id == item.Id)
                                       .AverageAsync(x => x.Rate);
                }
                else
                {
                    item.AverageVote = 0;
                }
            }

            var listy = list.AsEnumerable();

            if (!string.IsNullOrEmpty(queryParameters.Sort))
            {
                switch (queryParameters.Sort)
                {
                    case "nameDesc":
                        listy = listy.OrderByDescending(p => p.Name);
                        break;
                    case "rateDesc":
                        listy = listy.OrderByDescending(p => p.AverageVote);
                        break;
                    case "rateAsc":
                        listy = listy.OrderBy(p => p.AverageVote);
                        break;
                    case "practicingAsc":
                        listy = listy.OrderBy(p => p.StartedPracticing);
                        break;
                    case "practicingDesc":
                        listy = listy.OrderByDescending(p => p.StartedPracticing);
                        break;
                    default:
                        listy = listy.OrderBy(n => n.Name);
                        break;
                }
            }  
            return listy.ToList();        
        }

        public async Task<int> GetCountForAllDoctors()
        {
            return await _context.Doctors1.CountAsync();
        }

        public async Task<List<Doctor1>> GetFelipesDoctors(QueryParameters queryParameters)
        {
            var doctorSpecialization = await _context.DoctorSpecializations.
                                       Where(x => x.Specialization1Id == queryParameters.SpecializationId) 
                                       .FirstOrDefaultAsync();
            
            var doctors = _context.Doctors1.AsQueryable();

            if (queryParameters.HasQuery())
            {
                doctors = doctors
                .Where(x => x.Name.Contains(queryParameters.Query));
            }

           /*  if (queryParameters.SpecializationId.HasValue)
            {
                doctors = doctors.Where(x => x.Id == doctorSpecialization.Doctor1Id);
            } */
             return await doctors.ToListAsync();

        }

        public async Task<List<Specialization1>> GetAllSpecializations()
        {
            return await _context.Specializations.OrderBy(x => x.SpecializationName).ToListAsync();
        }

        public async Task<string> GetRoleName(int userId)
        {
            var roleName = await (from r in _context.Roles
                                  join ur in _context.UserRoles
                                  on r.Id equals ur.RoleId
                                  join u in _context.Users.Where(u => u.Id == userId)
                                  on ur.UserId equals u.Id
                                  select r.Name
                                 ).FirstOrDefaultAsync();

            return roleName;
        }

        public async Task<List<Doctor1>> ShowListOfAllDoctors()
        {
            return await _context.Doctors1.ToListAsync();
        }

        public async Task<List<Specialization1>> GetNonSelectedSpecializations(List<int> ids)
        {
            return await _context.Specializations.Where(x => !ids.Contains(x.Id)).ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
     

    }
}







