using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly PolyclinicContext _context;
        public PatientService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task CreatePatient1(ApplicationUser user, RegisterDto registerDto)
        {
            var patient = new Patient1();
            patient.ApplicationUserId = user.Id;
            patient.Name = string.Format("{0} {1}", user.FirstName, user.LastName);
            patient.DateOfBirth = registerDto.DateOfBirth;
            patient.Street = registerDto.Street;
            patient.City = registerDto.City;
            patient.Country = registerDto.Country;
            patient.MBO = registerDto.MBO;

            _context.Patients1.Add(patient);
            await _context.SaveChangesAsync();

        }

      
    }
}













