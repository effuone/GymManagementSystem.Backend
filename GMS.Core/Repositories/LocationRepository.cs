using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly GMSAppContext _context;

        public LocationRepository(GMSAppContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(Location model)
        {
            await _context.Locations.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.LocationId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Locations.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> GetAsync(int countryId, int cityId)
        {
            return await _context.Locations.Where(x=>x.CityId == cityId && x.CountryId == countryId).FirstOrDefaultAsync();
        }

        public async Task<Location> GetAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Location model)
        {
            var dbModel = await _context.Locations.FindAsync(id);
            model.CountryId = dbModel.CountryId;
            model.CityId = dbModel.CityId;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}