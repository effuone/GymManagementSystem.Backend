using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly GMSAppContext _context;

        public MembershipTypeRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(MembershipType model)
        {
            await _context.MembershipTypes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.MembershipTypeId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.MembershipTypes.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MembershipType>> GetAllAsync()
        {
            return await _context.MembershipTypes.ToListAsync();
        }

        public async Task<MembershipType> GetAsync(string membershipTypeName)
        {
            return await _context.MembershipTypes.Where(x=>x.MembershipTypeName == membershipTypeName).FirstOrDefaultAsync();
        }

        public async Task<MembershipType> GetAsync(int id)
        {
            return await _context.MembershipTypes.FindAsync(id);
        }

        public async Task UpdateAsync(int id, MembershipType model)
        {
            var dbModel = await _context.MembershipTypes.FindAsync(id);
            dbModel.MembershipTypeName = model.MembershipTypeName;
            _context.Update(dbModel);
            await _context.SaveChangesAsync();
        }
    }
}