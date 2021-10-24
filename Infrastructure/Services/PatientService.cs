using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task CreatePatient1(int userId, string lastname, string firstname, DateTime dateOfBirth)
        {
            var patient = new Patient1();
            patient.ApplicationUserId = userId;
            patient.Name = string.Format("{0} {1}", lastname, firstname);
            patient.DateOfBirth = dateOfBirth;

            _context.Patients1.Add(patient);
            await _context.SaveChangesAsync();

        }

      
    }
}













