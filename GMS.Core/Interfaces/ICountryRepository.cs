using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface ICountryRepository : IAsyncRepository<Country>
    {
        Task<Country> GetAsync(string countryName);
    }
}