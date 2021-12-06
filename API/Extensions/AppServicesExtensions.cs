using System.Linq;
using API.ErrorHandling;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ISubmissionFormService, SubmissionFormService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatient1Service, Patient1Service>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ITokenService, TokenService>();

            // ove dvije dolje su za slike
            // ne zaboravi dodati u startup app.UseStaticFiles();
            services.AddScoped<IFileStorageService, InAppStorageService>();
            services.AddHttpContextAccessor();

            services.AddDbContext<PolyclinicContext>(options =>
               options.UseSqlServer(
                   config.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.UseNetTopologySuite()));
            
            services.AddAutoMapper(typeof(MappingHelper).Assembly);

            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new MappingHelper(geometryFactory));
            }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices
               .Instance.CreateGeometryFactory(srid: 4326));

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