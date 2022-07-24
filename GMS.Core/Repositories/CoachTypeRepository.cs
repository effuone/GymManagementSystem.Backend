using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class CoachTypeRepository : ICoachTypeRepository
    {
        private readonly GMSAppContext _context;

        public CoachTypeRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(CoachType model)
        {
            await _context.CoachTypes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.CoachTypeId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.CoachTypes.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoachType>> GetAllAsync()
        {
            return await _context.CoachTypes.ToListAsync();
        }

        public async Task<CoachType> GetAsync(string coachTypeName)
        {
            return await _context.CoachTypes.Where(x=>x.CoachTypeName == coachTypeName).FirstOrDefaultAsync();
        }

        public async Task<CoachType> GetAsync(int id)
        {
            return await _context.CoachTypes.FindAsync(id);
        }

        public async Task UpdateAsync(int id, CoachType model)
        {
            var dbModel = await _context.CoachTypes.FindAsync(id);
            dbModel.CoachTypeName = model.CoachTypeName;
            _context.Update(dbModel);
            await _context.SaveChangesAsync();
        }
    }
}