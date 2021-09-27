using System.Linq;
using API.ErrorHandling;
using API.Services;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ISubmissionFormService, SubmissionFormService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddAutoMapper(typeof(MappingHelper).Assembly);

            services.AddDbContext<PolyclinicContext>(options =>
               options.UseSqlServer(
                   config.GetConnectionString("DefaultConnection")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    
                    var errorResponse = new ServerValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }

    }
}