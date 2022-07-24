using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly GMSAppContext _context;

        public CountryRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Country model)
        {
            await _context.Countries.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CountryId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Countries.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetAsync(string countryName)
        {
            return await _context.Countries.Where(x=>x.CountryName == countryName).FirstOrDefaultAsync();
        }

        public async Task<Country> GetAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Country model)
        {
            var dbModel = await _context.Countries.FindAsync(id);
            dbModel.CountryName = model.CountryName;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}