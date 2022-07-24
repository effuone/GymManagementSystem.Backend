using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface ICoachRepository : IAsyncRepository<Coach>
    {
        Task<Coach> GetAsync(string userId);
    }
}