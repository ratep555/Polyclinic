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

            doctor.PolyclinicDepartmentId = registerDoctorDto.PolyclinicDepartmentId;
            doctor.SpecialtyId = registerDoctorDto.SpecialtyId;

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

        }

        public async Task CreateEmployee(int userId, RegisterEmployeeDto registerEmployeeDto,
            string firstname, string lastname)
        {
            var employee = new Employee();
            employee.ApplicationUserId = userId;
            employee.Name = string.Format("{0} {1}", firstname, lastname);

            employee.PolyclinicDepartmentId = registerEmployeeDto.PolyclinicDepartmentId;
            employee.CategoryId = registerEmployeeDto.CategoryId;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

        }
    }
}







