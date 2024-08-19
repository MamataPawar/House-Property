
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        public CityController(){

        }

        [HttpGet]
        public  IEnumerable<string> GetCity()
        {
            return new string[] { "mamata" };
        }
        
    }
}