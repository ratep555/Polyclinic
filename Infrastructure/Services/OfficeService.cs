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
    public class OfficeService : IOfficeService
    {
        private readonly PolyclinicContext _context;
        public OfficeService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task<List<Office1>> GetOfficesWithSearchingAndPaging(QueryParameters queryParameters, 
            int userId)
        {
            var doctor = await _context.Doctors1.Where(x => x.ApplicationUserId == userId)
                               .FirstOrDefaultAsync();

            IQueryable<Office1> office = _context.Offices.Where(x => x.Doctor1Id == doctor.Id)
                                         .AsQueryable().OrderBy(x => x.Id);
            
            if (queryParameters.HasQuery())
            {
                office = office
                .Where(x => x.City.Contains(queryParameters.Query));
            }

            office = office.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await office.ToListAsync();        
        }

        public async Task<int> GetCountForOffices(int userId)
        {
             var doctor = await _context.Doctors1.Where(x => x.ApplicationUserId == userId)
                               .FirstOrDefaultAsync();

            return await _context.Offices.Where(x => x.Doctor1Id == doctor.Id).CountAsync();
        }

        public async Task<Office1> GetOfficeByIdAsync(int id)
        {
            return await _context.Offices.Include(x => x.Doctor)        
                         .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateOffice(Office1 office)
        {
            // var doctor = await _context.Doctors1.Where(x => x.ApplicationUserId == id)
                             //  .FirstOrDefaultAsync();
            
           // var office = new Office1();
           // office.Doctor1Id = doctor.Id;
            // ovo ostalo do add ti nepotrebno zbog mapiranja u controlleru, ne trebaš dto u parametru
            // uostalom pogledaj myportfolio
            /* office.InitialExaminationFee = officeDto.InitialExaminationFee;
            office.FollowUpExaminationFee = officeDto.FollowUpExaminationFee;
            office.Street = officeDto.Street;
            office.City = officeDto.City;
            office.Country = officeDto.Country;
 */
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOffice(Office1 office)
        {
            _context.Entry(office).State = EntityState.Modified;        
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(Appointment1 appointment)
        {
            
             _context.Entry(appointment).State = EntityState.Modified;        
             await _context.SaveChangesAsync();
        }

        public async Task<Doctor1> FindDoctorById(int userId)
        {
            return await _context.Doctors1.Where(x => x.ApplicationUserId == userId)
                         .FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


    }
}









