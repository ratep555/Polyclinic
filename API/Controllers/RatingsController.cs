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
        public async Task<ActionResult> CreateRating([FromBody] RatingDto ratingDto)
        {
            var userId = User.GetUserId();

            if (await _ratingService.CheckIfThisIsDoctorsPatient(ratingDto.DoctorId, userId))
            {
                return BadRequest("You have not visited this doctor yet!");            
            }

            var currentRate = await _ratingService.FindCurrentRate(ratingDto.DoctorId, userId);

            if (currentRate == null)
            {
                await _ratingService.AddRating(ratingDto, userId);
            }
            else
            {
                currentRate.Rate = ratingDto.Rating;
                await _ratingService.Save();
            }      
            return NoContent();
        }
    }
}
    




    