using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var Cities = await uow.CityRepository.GetCitiesAsync();
            var cityDto = mapper.Map<IEnumerable<CityDto>>(Cities);
            // var cityDto = from c in Cities
            //               select new CityDto()
            //               {
            //                   Id = c.Id,
            //                   Name = c.Name
            //               };

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
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedOn = DateTime.Now;
            city.LastupdatedBy = 1;

            // var city = new City()
            // {
            //     Name = cityDto.Name,
            //     LastUpdatedOn = DateTime.Now,
            //     LastupdatedBy = 1
            // };

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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            if (id != cityDto.Id)
                return BadRequest("Update not allowed");

            var cityFromDB = uow.CityRepository.FindCity(id).Result;

            if (cityFromDB == null)
                return BadRequest("Update not allowed");

            cityFromDB.LastUpdatedOn = DateTime.Now;
            cityFromDB.LastupdatedBy = 1;

            mapper.Map(cityDto, cityFromDB);

            throw new Exception("Something happended");
            await uow.SaveAsync();

            return StatusCode(200);
        }

        [HttpPatch("patch/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var cityFromDB = uow.CityRepository.FindCity(id).Result;
            cityFromDB.LastUpdatedOn = DateTime.Now;
            cityFromDB.LastupdatedBy = 1;

            cityToPatch.ApplyTo(cityFromDB, ModelState);

            await uow.SaveAsync();

            return StatusCode(200);
        }
    }
}