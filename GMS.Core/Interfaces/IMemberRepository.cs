using GMS.Data.Models;

namespace GMS.Core.Interfaces
{
    public interface IMemberRepository: IAsyncRepository<Member>
    {
        Task<Member> GetAsync(string userId);
    }
}