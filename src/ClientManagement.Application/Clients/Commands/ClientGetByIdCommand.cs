using BuildBlocks.Core.Messages;
using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Repositories;
using FluentResults;

namespace ClientManagement.Application.Clients.Commands
{
    public record ClientGetByIdCommand(string Id) : ICommand<Result<Client>>;
    public class ClientGetByIdCommandHandler : ICommandHandler<ClientGetByIdCommand, Result<Client>>
    {
        private readonly IClientRepository repository;
        public ClientGetByIdCommandHandler(IClientRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Result<Client>> Handle(ClientGetByIdCommand request, CancellationToken cancellationToken)
        {
            var client = await repository.FindAsync(request.Id, cancellationToken);
            if (client is null)
            {
                return Result.Fail("Unable to find given Client");
            }
            return client;
        }
    }
}
