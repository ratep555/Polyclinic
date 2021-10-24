using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RatingsController : BaseApiController
    {
        private readonly IRatingService _ratingService;
        private readonly IMapper _mapper;
        public RatingsController(IRatingService ratingService, IMapper mapper)
        {
            _ratingService = ratingService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRating([FromBody] RatingDto ratingDtO)
        {
            var userId = User.GetUserId();

            var currentRate = await _ratingService.FindCurrentRate(ratingDtO.DoctorId, userId);

            if (currentRate == null)
            {
                await _ratingService.AddRating(ratingDtO, userId);
            }
            else
            {
                currentRate.Rate = ratingDtO.Rating;
                await _ratingService.Save();
            }      
            return NoContent();
        }
    }
}
    




    