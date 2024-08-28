using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;
        public CityRepository(DataContext dc)
        {
            this.dc = dc;
            
        }
        public void AddCity(City city)
        {
            dc.Cities.Add(city);
        }

        public void DeleteCity(int id)
        {
            var city = dc.Cities.Find(id);
            if (city != null)
                dc.Cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dc.Cities.ToListAsync();
        }

        public async Task<bool> SaveAsyc()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}