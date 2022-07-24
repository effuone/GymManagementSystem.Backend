using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface ILocationRepository: IAsyncRepository<Location>
    {
        public Task<Location> GetAsync(int countryId, int cityId);
    }
}