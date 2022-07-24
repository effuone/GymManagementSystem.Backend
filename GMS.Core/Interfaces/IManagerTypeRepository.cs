using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface IManagerTypeRepository : IAsyncRepository<ManagerType>
    {
        Task<ManagerType> GetAsync(string managerTypeName);
    }
}