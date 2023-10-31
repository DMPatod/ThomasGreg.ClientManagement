using BuildBlocks.Core.Messages;
using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Repositories;
using FluentResults;

namespace ClientManagement.Application.Clients.Commands
{
    public record ClientGetAllCommand() : ICommand<Result<IEnumerable<Client>>>;
    public class ClientGetAllCommandHandler : ICommandHandler<ClientGetAllCommand, Result<IEnumerable<Client>>>
    {
        private readonly IClientRepository repository;
        public ClientGetAllCommandHandler(IClientRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Result<IEnumerable<Client>>> Handle(ClientGetAllCommand request, CancellationToken cancellationToken)
        {
            var clients = await repository.GetAsync(cancellationToken);
            return clients.ToList();
        }
    }
}
