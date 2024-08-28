using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Repo;
using WebAPI.Models;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository repo;

        public CityController(ICityRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var Cities = await repo.GetCitiesAsync();
            return Ok(Cities);
        }

        // //Post api/city/add?cityName=Mumbai
        // [HttpPost("add/{cityName}")]
        // [HttpPost("add")]
        // public async Task<IActionResult> AddCity(string cityName)
        // {
        //     City city = new City() { Name=cityName };
        //     await dc.Cities.AddAsync(city);
        //     await dc.SaveChangesAsync();            

        //     return Ok(city);
        // }
        
        [HttpPost("Post")]
        public async Task<IActionResult> AddCity(City city)
        {
            repo.AddCity(city);
            await repo.SaveAsyc();

            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            repo.DeleteCity(id);
            await repo.SaveAsyc();

            return Ok(id);
        }
    }
}