using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IPatientService
    {
        Task CreatePatient1(int userId, string lastname, string firstname);


    }
}