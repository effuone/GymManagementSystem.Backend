using GMS.Core.Interfaces;
using GMS.Data.Context;
using GMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS.Core.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GMSAppContext _context;

        public MemberRepository(GMSAppContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Member model)
        {
            await _context.Members.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.MemberId;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Members.FindAsync(id);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<Member> GetAsync(string userId)
        {
            return await _context.Members.Where(x=>x.GMSUserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, Member model)
        {
            var dbModel = await _context.Members.FindAsync(id);
            dbModel.MembershipTypeId = model.MembershipTypeId;
            dbModel.GymId = model.GymId;
            dbModel.GMSUserId = model.GMSUserId;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}