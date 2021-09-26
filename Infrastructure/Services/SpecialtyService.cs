using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly PolyclinicContext _context;
        public SpecialtyService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task<List<Specialty>> GetSpecialtiesWithSearchingAndPaging(QueryParameters queryParameters)
        {
            IQueryable<Specialty> specialties = _context.Specialties.AsQueryable()
                                            .OrderBy(x => x.SpecialtyName);
            
            if (queryParameters.HasQuery())
            {
                specialties = specialties
                .Where(t => t.SpecialtyName.Contains(queryParameters.Query));
            }

            specialties = specialties.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await specialties.ToListAsync();        
        }

        public async Task<int> GetCountForSpecialties()
        {
            return await _context.Specialties.CountAsync();
        }

        public async Task<Specialty> GetSpecialtyByIdAsync(int id)
        {
            return await _context.Specialties         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateSpecialty(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            await _context.SaveChangesAsync();                    
        }

        public async Task UpdateSpecialty(Specialty specialty)
        {
            _context.Entry(specialty).State = EntityState.Modified;        
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpecialty(Specialty specialty)
        {
            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
        }

       
    }
}






