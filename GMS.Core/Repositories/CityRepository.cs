using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly GMSAppContext _context;

        public CityRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(City model)
        {
            await _context.Cities.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CityId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Locations.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAllAsync(string countryName)
        {
            var locations = await _context.Locations.Where(x=>x.Country.CountryName == countryName).ToListAsync();
            var cities = new List<City>();
            foreach(var location in locations)
            {
                cities.Add(location.City);
            }
            return cities;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City> GetAsync(string cityName)
        {
            return await _context.Cities.Where(x=>x.CityName == cityName).FirstOrDefaultAsync();
        }

        public async Task<City> GetAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task UpdateAsync(int id, City model)
        {
            var dbModel = await _context.Cities.FindAsync(id);
            dbModel.CityName = model.CityName;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}