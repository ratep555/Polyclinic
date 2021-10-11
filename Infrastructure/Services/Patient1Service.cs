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
            IEnumerable<int> ids = doctorSpecializations.Select(x => x.Id);

            var specializations = await _context.Specializations.Where(x => ids.Contains(x.Id)).ToListAsync();
            return specializations;
        }

          public async Task<int> GetCountForOffices()
        {
            return await _context.Offices.CountAsync();
        }


    }
}







