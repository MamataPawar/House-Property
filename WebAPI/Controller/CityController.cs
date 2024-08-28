
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly DataContext dc;
        public CityController(DataContext dataContext)
        {
            this.dc = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var Cities = await dc.Cities.ToListAsync();
            return Ok(Cities);
        }
        
    }
}