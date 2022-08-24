using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentTask.Models;
using RecruitmentTask.Models.Responses;
using RecruitmentTask.Services;

namespace RecruitmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientsController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ClientDto>>> GetAllAsync()
        {
            var result = await clientService.GetClientsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetAsync(int id)
        {
            var result = await clientService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound($"Client with id ${id} has not been found");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> AddAsync(ClientAddEditModel client)
        {
            if (string.IsNullOrEmpty(client.Email)) return BadRequest($"Field ${nameof(ClientAddEditModel.Email)} must be provided");
            if (string.IsNullOrEmpty(client.FirstName)) return BadRequest($"Field ${nameof(ClientAddEditModel.FirstName)} must be provided");
            if (string.IsNullOrEmpty(client.LastName)) return BadRequest($"Field ${nameof(ClientAddEditModel.LastName)} must be provided");

            var result = await clientService.AddClientAsync(client);

            return GetParsedResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientDto>> UpdateAsync([FromQuery]int id, ClientAddEditModel client)
        {
            //I assumed that frontend sent all, even not changed properties
            if (string.IsNullOrEmpty(client.Email)) return BadRequest($"Field ${nameof(ClientAddEditModel.Email)} must be provided");
            if (string.IsNullOrEmpty(client.FirstName)) return BadRequest($"Field ${nameof(ClientAddEditModel.FirstName)} must be provided");
            if (string.IsNullOrEmpty(client.LastName)) return BadRequest($"Field ${nameof(ClientAddEditModel.LastName)} must be provided");

            var result = await clientService.UpdateClientAsync(id, client);

            return GetParsedResult(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveAsync([FromQuery]int id)
        {
            var result = await clientService.RemoveClientAsync(id);

            return Ok();
        }

        private ActionResult<ClientDto> GetParsedResult(ResponseBaseClass<ClientDto> response)
        {
            if(response is SuccessResponse<ClientDto> successResponse)
            {
                return Ok(successResponse.Result);
            }
            else if(response is ErrorResponse<ClientDto> errorResponse)
            {
                return StatusCode(errorResponse.StatusCode, errorResponse.GetMessage());
            }

            throw new Exception($"TYpe of response: {response.GetType().Name} has not been recognized");
        }
    }
}
