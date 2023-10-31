using BuildBlocks.Core.Messages;
using ClientManagement.Application.Names;
using ClientManagement.Application.PublicThoroughfares;
using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Entities;
using ClientManagement.Domain.Clients.Repositories;
using ClientManagement.Domain.Clients.ValueObjects;
using FluentResults;

namespace ClientManagement.Application.Clients.Commands
{
    public record ClientCreateCommand(NameCreateCommand Name, string Email, string Logo, ICollection<PublicThoroughfareCreateCommand> PublicThoroughfares) : ICommand<Result<Client>>;

    public class ClientCreateCommandHandler : ICommandHandler<ClientCreateCommand, Result<Client>>
    {
        private readonly IClientRepository repository;
        public ClientCreateCommandHandler(IClientRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Result<Client>> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
        {
            var publicThoroughfares = request.PublicThoroughfares.Select(pt => PublicThoroughfare.Create(
                pt.Street,
                pt.Number,
                pt.City,
                pt.State,
                pt.AditionalInformation)).ToList();

            var client = await repository.CreateAsync(
                Client.Create(
                    Name.Create(
                        request.Name.FirstName,
                        request.Name.LastName),
                    request.Email,
                    request.Logo,
                    publicThoroughfares), cancellationToken);

            return client;
        }
    }
}
