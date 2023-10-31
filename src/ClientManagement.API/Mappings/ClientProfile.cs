using AutoMapper;
using ClientManagement.API.Contracts.Clients;
using ClientManagement.API.Contracts.Names;
using ClientManagement.API.Contracts.PublicThoroughfares;
using ClientManagement.Application.Clients.Commands;
using ClientManagement.Application.Names;
using ClientManagement.Application.PublicThoroughfares;
using ClientManagement.Domain.Clients;
using ClientManagement.Domain.Clients.Entities;
using ClientManagement.Domain.Clients.ValueObjects;

namespace ClientManagement.API.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientResponse>().ReverseMap();
            CreateMap<Name, NameResponse>().ReverseMap();
            CreateMap<PublicThoroughfare, PublicThoroughfareResponse>().ReverseMap();

            CreateMap<ClientCreateRequest, ClientCreateCommand>();
            CreateMap<NameCreateRequest, NameCreateCommand>();
            CreateMap<PublicThoroughfareCreateRequest, PublicThoroughfareCreateCommand>();
        }
    }
}
