using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface IManagerRepository : IAsyncRepository<Manager>
    {
        Task<Manager> GetAsync(string userId);
    }
}