using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRatingService
    {
        Task<Rating> FindCurrentRate(int doctorId, int userId);
        Task AddRating(RatingDto ratingDto, int userId);
        Task Save();


    }
}