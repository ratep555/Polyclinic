using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public async Task CreateDoctor1(int userId, string lastname, string firstname)
        {
            var doctor = new Doctor1();
            doctor.ApplicationUserId = userId;
            doctor.Name = string.Format("{0} {1}", lastname, firstname);

            _context.Doctors1.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<Doctor1> FindDoctorById(int id)
        {
            return await _context.Doctors1.Where(x => x.Id == id).FirstOrDefaultAsync();
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
        public async Task<List<Office1>> GetOfficesForDoctor(int id)
        {

            var doctor = await _context.Doctors1.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            var offices = await _context.Offices.Where(x => x.Doctor1Id == doctor.Id).ToListAsync();

            return offices;
        }

       
    }
}







