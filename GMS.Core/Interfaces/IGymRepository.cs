using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface IGymRepository : IAsyncRepository<Gym>
    {
        Task<Gym> GetAsync(string gymName);
        Task<IEnumerable<Gym>> GetAsync(Location location);
    }
}