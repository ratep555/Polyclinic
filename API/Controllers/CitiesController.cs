using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        public string GetCities() 
        {
            return "hi";
        }
        
    }
}