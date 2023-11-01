using Asp.Versioning;
using AutoMapper;
using BuildBlocks.Core.DomainEvents;
using ClientManagement.API.Contracts.Clients;
using ClientManagement.Application.Clients.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IDomainMessageHandler messageHandler;
        private readonly IMapper mapper;
        public ClientController(IDomainMessageHandler messageHandler, IMapper mapper)
        {
            this.messageHandler = messageHandler;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new ClientGetAllCommand();
            var result = await messageHandler.SendAsync(command, CancellationToken.None);
            if (result.IsSuccess)
            {
                var response = mapper.Map<List<ClientResponse>>(result.Value);
                return Ok(response);
            }

            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var command = new ClientGetByIdCommand(id);
            var result = await messageHandler.SendAsync(command, CancellationToken.None);
            if (result.IsSuccess)
            {
                var response = mapper.Map<ClientResponse>(result.Value);
                return Ok(response);
            }

            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateRequest request)
        {
            var command = mapper.Map<ClientCreateCommand>(request);
            var result = await messageHandler.SendAsync(command, CancellationToken.None);
            if (result.IsSuccess)
            {
                var response = mapper.Map<ClientResponse>(result.Value);
                var url = Url.Action(nameof(GetById), new { id = response.Id }) ?? $"/{response.Id}";
                return Created(url, response);
            }

            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ClientUpdateRequest request)
        {
            var command = mapper.Map<ClientUpdateCommand>(request);
            var result = await messageHandler.SendAsync(command, CancellationToken.None);
            if (result.IsSuccess)
            {
                var response = mapper.Map<ClientResponse>(result.Value);
                return Ok(response);
            }

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new ClientDeleteCommand(id);
            var result = await messageHandler.SendAsync(command, CancellationToken.None);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
