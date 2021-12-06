using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Dtos;

namespace Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly PolyclinicContext _context;
        public RatingService(PolyclinicContext context)
        {
            _context = context;
        }

        public async Task<Rating> FindCurrentRate(int doctorId, int userId)
        {
            return await _context.Ratings.Include(x => x.Patient)
                   .Where(x => x.Doctor1Id == doctorId && x.Patient.ApplicationUserId == userId).FirstOrDefaultAsync();
        }

        public async Task AddRating(RatingDto ratingDto, int userId)
        {
            var patient = await _context.Patients1.Where(x => x.ApplicationUserId == userId)
                          .FirstOrDefaultAsync();

            var rating = new Rating();
            rating.Doctor1Id = ratingDto.DoctorId;
            rating.Rate = ratingDto.Rating;
            rating.Patient1Id = patient.Id;

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIfThisIsDoctorsPatient(int id, int userId)
        {
            var records = await _context.MedicalRecords1.Include(x => x.Appointment)
            .ThenInclude(x => x.Office).ThenInclude(x => x.Doctor)
                .Include(x => x.Appointment).ThenInclude(x => x.Patient)
                .Where(x => x.Appointment.Office.Doctor.Id == id && x.Appointment.Patient.ApplicationUserId == userId).ToListAsync();

           if (!records.Any())
           {
               return true;
           }

           return false;
        }
    }
}










