using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPatientService
    {
        Task CreatePatient1(ApplicationUser user, RegisterDto registerDto);
        Task<List<Gender>> GetGenders();



    }
}