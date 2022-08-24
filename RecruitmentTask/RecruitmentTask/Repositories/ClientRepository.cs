using RecruitmentTask.Models;
using System.Collections.Concurrent;

namespace RecruitmentTask.Repositories
{
    public class ClientRepository : IClientRepository
    {
        ConcurrentDictionary<int, ClientDto> _data = new ConcurrentDictionary<int, ClientDto>();

        public async Task<ClientDto> AddAsync(ClientAddEditModel newClient)
        {
            var currentMaxId = !_data.Keys.Any() ? 0 : _data.Keys.Max();

            var newEntity = new ClientDto
            {
                Id = currentMaxId + 1,
                Email = newClient.Email,
                FirstName = newClient.FirstName,
                LastName = newClient.LastName,
            };

            _data.TryAdd(currentMaxId + 1, newEntity);

            return newEntity;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            return _data.Select(d => d.Value).ToList();
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            _data.TryGetValue(id, out var result);

            return result;
        }

        public async Task<bool> GetByPredicateAsync(Func<ClientDto, bool> predicate)
        {
            return _data.Select(d => d.Value).Where(predicate).Any();
        }

        public async Task UpdateAsync(ClientDto client)
        {
            _data.TryGetValue(client.Id, out var result);

            _data.TryUpdate(client.Id, client, result!);
        }
    }
}
