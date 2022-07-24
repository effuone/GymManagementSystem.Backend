using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class ManagerTypeRepository : IManagerTypeRepository
    {
        private readonly GMSAppContext _context;

        public ManagerTypeRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(ManagerType model)
        {
            await _context.ManagerTypes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.ManagerTypeId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.ManagerTypes.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ManagerType>> GetAllAsync()
        {
            return await _context.ManagerTypes.ToListAsync();
        }

        public async Task<ManagerType> GetAsync(int id)
        {
            return await _context.ManagerTypes.FindAsync(id);
        }

        public async Task<ManagerType> GetAsync(string managerTypeName)
        {
            return await _context.ManagerTypes.Where(x=>x.ManagerTypeName == managerTypeName).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, ManagerType model)
        {
            var dbModel = await _context.ManagerTypes.FindAsync(id);
            dbModel.ManagerTypeName = model.ManagerTypeName;
            _context.Update(dbModel);
            await _context.SaveChangesAsync();
        }
    }
}