using BuildBlocks.Core.Messages;
using ClientManagement.Application.Names;
using ClientManagement.Application.PublicThoroughfares;
using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Entities;
using ClientManagement.Domain.Clients.Repositories;
using FluentResults;

namespace ClientManagement.Application.Clients.Commands
{
    public record ClientUpdateCommand(NameCommand Name, string Email, string Logo, ICollection<PublicThoroughfareUpdateCommand> PublicThoroughfares)
        : ICommand<Result<Client>>
    {
        public string Id { get; set; }
    }
    public class ClientUpdateCommandHandler : ICommandHandler<ClientUpdateCommand, Result<Client>>
    {
        public IClientRepository repository;

        public ClientUpdateCommandHandler(IClientRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Client>> Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
        {
            var thoroughfares = request.PublicThoroughfares.Select(pt => PublicThoroughfare.Create(pt.Street, pt.Number, pt.City, pt.State, pt.AditionalInformation));

            var client = await repository.FindAsync(request.Id, cancellationToken);
            if (client is null)
            {
                return Result.Fail("Unable to find Client");
            }

            client.Name.FirstName = request.Name.FirstName;
            client.Name.LastName = request.Name.LastName;
            client.Email = request.Email;
            client.Logo = request.Logo;

            foreach (var item in request.PublicThoroughfares)
            {
                var match = client.PublicThoroughfares.SingleOrDefault(x => x.Id.ToString() == item.Id);
                if (match is null)
                {
                    client.AddPublicThoroughfare(PublicThoroughfare.Create(item.Street, item.Number, item.City, item.State, item.AditionalInformation));
                }
                else
                {
                    match.Update(item.Street, item.Number, item.City, item.State, item.AditionalInformation);
                }
            }

            client = await repository.UpdateAsync(client, cancellationToken);
            return client;
        }
    }
}
