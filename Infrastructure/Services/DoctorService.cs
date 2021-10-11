using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

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

       
    }
}







