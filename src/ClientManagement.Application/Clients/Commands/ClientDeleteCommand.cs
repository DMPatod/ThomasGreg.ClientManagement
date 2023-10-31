using BuildBlocks.Core.Messages;
using ClientManagement.Domain.Clients.Repositories;
using FluentResults;

namespace ClientManagement.Application.Clients.Commands
{
    public record ClientDeleteCommand(string Id) : ICommand<Result>;
    public class ClientDeleteCommandHandler : ICommandHandler<ClientDeleteCommand, Result>
    {
        private readonly IClientRepository repository;

        public ClientDeleteCommandHandler(IClientRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
        {
            if (await repository.DeleteAsync(request.Id, cancellationToken))
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Fail to delete client!");
            }
        }
    }

}
