using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface ICityRepository: IAsyncRepository<City>
    {
        Task<City> GetAsync(string cityName);
        Task<IEnumerable<City>> GetAllAsync(string countryName);
    }
}