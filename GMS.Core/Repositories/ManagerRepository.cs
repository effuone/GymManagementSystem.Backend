using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly GMSAppContext _context;

        public ManagerRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Manager model)
        {
            await _context.Managers.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.ManagerId;

        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Managers.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await _context.Managers.ToListAsync();
        }

        public async Task<Manager> GetAsync(string userId)
        {
            return await _context.Managers.Where(x=>x.GMSUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Manager> GetAsync(int id)
        {
            return await _context.Managers.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Manager model)
        {
            var dbModel = await _context.Managers.FindAsync(id);
            model.ManagerId = dbModel.ManagerId;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}