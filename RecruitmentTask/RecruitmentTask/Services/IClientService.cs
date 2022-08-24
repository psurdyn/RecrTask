using RecruitmentTask.Models;
using RecruitmentTask.Models.Responses;

namespace RecruitmentTask.Services
{
    public interface IClientService
    {
        Task<IList<ClientDto>> GetClientsAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task<ResponseBaseClass<ClientDto>> AddClientAsync(ClientAddEditModel newClient);
        Task<ResponseBaseClass<ClientDto>> UpdateClientAsync(int id, ClientAddEditModel editedClient);
    }
}
