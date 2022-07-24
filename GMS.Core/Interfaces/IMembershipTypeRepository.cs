using GMS.Core.Interfaces;
using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface IMembershipTypeRepository : IAsyncRepository<MembershipType>
    {
        Task<MembershipType> GetAsync(string membershipTypeName);
    }
}