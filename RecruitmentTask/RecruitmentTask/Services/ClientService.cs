using RecruitmentTask.Models;
using RecruitmentTask.Models.Responses;
using RecruitmentTask.Repositories;

namespace RecruitmentTask.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<ResponseBaseClass<ClientDto>> AddClientAsync(ClientAddEditModel newClient)
        {
            var clientWithTHeSameEmail = await clientRepository.GetByPredicateAsync(x => x.Email == newClient.Email);
            if (clientWithTHeSameEmail)
            {
                return new ErrorResponse<ClientDto>(400, new ArgumentException($"Client with the same email address already exists"));
            }

            var createdEntity = await clientRepository.AddAsync(newClient);

            return new SuccessResponse<ClientDto>(createdEntity);
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            return await clientRepository.GetByIdAsync(id);
        }

        public async Task<IList<ClientDto>> GetClientsAsync()
        {
            return await clientRepository.GetAllAsync();
        }

        public async Task<ResponseBaseClass<bool>> RemoveClientAsync(int id)
        {
            var isEntityExists = await clientRepository.GetByPredicateAsync(x => x.Id == id);
            if(!isEntityExists)
            {
                return new ErrorResponse<bool>(404, $"Client with id: {id} has not been found");
            }

            await clientRepository.RemoveAsync(id);

            return new SuccessResponse<bool>(true);
        }

        public async Task<ResponseBaseClass<ClientDto>> UpdateClientAsync(int id, ClientAddEditModel editedClient)
        {
            var entity = await clientRepository.GetByPredicateAsync(x => x.Id == id);
            if(entity == false)
            {
                return new ErrorResponse<ClientDto>(404, $"Client with id: {id} has not been found");
            }

            var entityToUpdate = new ClientDto
            {
                Id = id,
                Email = editedClient.Email,
                FirstName = editedClient.FirstName,
                LastName = editedClient.LastName,
            };
            await clientRepository.UpdateAsync(entityToUpdate);

            return new SuccessResponse<ClientDto>(entityToUpdate);
        }
    }
}
