using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly GMSAppContext _context;

        public GymRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Gym model)
        {
            await _context.Gyms.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.GymId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Gyms.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Gym>> GetAllAsync()
        {
            return await _context.Gyms.ToListAsync();
        }

        public async Task<Gym> GetAsync(string gymName)
        {
            return await _context.Gyms.Where(x=>x.GymName == gymName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Gym>> GetAsync(Location location)
        {
            return await _context.Gyms.Where(x=>x.LocationId == location.LocationId).ToListAsync();
        }
        public async Task<Gym> GetAsync(int id)
        {
            return await _context.Gyms.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Gym model)
        {
            var dbModel = await _context.Gyms.FindAsync(id);
            dbModel.GymName = model.GymName;
            dbModel.Image = model.Image;
            dbModel.Location.LocationId = model.Location.LocationId;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}