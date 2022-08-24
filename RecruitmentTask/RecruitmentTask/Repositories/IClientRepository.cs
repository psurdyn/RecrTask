using RecruitmentTask.Models;

namespace RecruitmentTask.Repositories
{
    public interface IClientRepository
    {
        Task<List<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task<bool> GetByPredicateAsync(Func<ClientDto, bool> predicate);
        Task<ClientDto> AddAsync(ClientAddEditModel newClient);
        Task UpdateAsync(ClientDto client);
        Task RemoveAsync(int id);
    }
}
