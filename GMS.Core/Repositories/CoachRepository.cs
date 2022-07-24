using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class CoachRepository : ICoachRepository
    {
        private readonly GMSAppContext _context;

        public CoachRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Coach model)
        {
            await _context.Coaches.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CoachId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Coaches.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Coach>> GetAllAsync()
        {
            return await _context.Coaches.ToListAsync();
        }

        public async Task<Coach> GetAsync(string userId)
        {
            return await _context.Coaches.Where(x=>x.GMSUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Coach> GetAsync(int id)
        {
            return await _context.Coaches.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Coach model)
        {
            var dbModel = await _context.Coaches.FindAsync(id);
            model.CoachId = dbModel.CoachId;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}