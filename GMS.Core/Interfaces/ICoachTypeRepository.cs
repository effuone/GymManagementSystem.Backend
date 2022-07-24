using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface ICoachTypeRepository : IAsyncRepository<CoachType>
    {
        Task<CoachType> GetAsync(string coachTypeName);
    }
}