using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class PolyclinicContextSeed
    {
        public static async Task SeedUserAsync(
        UserManager<ApplicationUser> userManager, 
        RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole{Name = "Visitor"},
                new ApplicationRole{Name = "Patient"},
                new ApplicationRole{Name = "Admin"},
                new ApplicationRole{Name = "Doctor"},
                new ApplicationRole{Name = "Employee"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            if (await userManager.Users.AnyAsync()) return;
            
            var admin = new ApplicationUser
            {
                FirstName = "Bob",
                LastName = "Bobbity",
                Email = "bob@test.com",
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");               
            await userManager.AddToRolesAsync(admin, new[] {"Admin"});                                        
        }

        public static async Task SeedEntitiesAsync(PolyclinicContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Countries.Any())
                {
                    var countriesData = File.ReadAllText("../Infrastructure/Data/SeedData/countries.json");
                    var countries = JsonSerializer.Deserialize<List<Country>>(countriesData);

                    foreach (var item in countries)
                    {
                        context.Countries.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Cities.Any())
                {
                    var citiesData = File.ReadAllText("../Infrastructure/Data/SeedData/cities.json");
                    var cities = JsonSerializer.Deserialize<List<City>>(citiesData);

                    foreach (var item in cities)
                    {
                        context.Cities.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Addresses.Any())
                {
                    var addressesData = File.ReadAllText("../Infrastructure/Data/SeedData/addresses.json");
                    var addresses = JsonSerializer.Deserialize<List<Address>>(addressesData);

                    foreach (var item in addresses)
                    {
                        context.Addresses.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Polyclinics.Any())
                {
                    var polyclinicsData = File.ReadAllText("../Infrastructure/Data/SeedData/polyclinics.json");
                    var polyclinics = JsonSerializer.Deserialize<List<Polyclinic>>(polyclinicsData);

                    foreach (var item in polyclinics)
                    {
                        context.Polyclinics.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

               /*  if (!context.Departments.Any())
                {
                    var departmentsData = File.ReadAllText("../Infrastructure/Data/SeedData/departments.json");
                    var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);

                    foreach (var item in departments)
                    {
                        context.Departments.Add(item);
                    }

                    await context.SaveChangesAsync();
                } */

                if (!context.PrefferedTimeOfExaminations.Any())
                {
                    var prefferenceData = File.ReadAllText("../Infrastructure/Data/SeedData/prefferedTimeOfExaminations.json");
                    var prefferences = JsonSerializer.Deserialize<List<PrefferedTimeOfExamination>>(prefferenceData);

                    foreach (var item in prefferences)
                    {
                        context.PrefferedTimeOfExaminations.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

              /*   if (!context.Specialties.Any())
                {
                    var specialtiesData = File.ReadAllText("../Infrastructure/Data/SeedData/specialties.json");
                    var specialties = JsonSerializer.Deserialize<List<Specialty>>(specialtiesData);

                    foreach (var item in specialties)
                    {
                        context.Specialties.Add(item);
                    }

                    await context.SaveChangesAsync();
                } */

            
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<PolyclinicContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
