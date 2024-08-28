using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var Cities = await uow.CityRepository.GetCitiesAsync();

            var cityDto = from c in Cities
                          select new CityDto()
                          {
                              Id = c.Id,
                              Name = c.Name
                          };

            return Ok(cityDto);
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
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = new City()
            {
                Name = cityDto.Name,
                LastUpdatedOn = DateTime.Now,
                LastupdatedBy = 1
            };

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();

            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();

            return Ok(id);
        }
    }
}