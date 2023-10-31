using BuildBlocks.Core.DomainEvents;
using BuildBlocks.Core.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace ClientManagement.Application.Commons
{
    public class DomainMessageHandler : IDomainMessageHandler
    {
        private readonly IMediator mediator;
        public DomainMessageHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public Task PublishAsync<T>(T domainEvent, CancellationToken cancellationToken) where T : IDomainEvent
        {
            return mediator.Publish(domainEvent, cancellationToken);
        }

        public Task SendAsync(ICommand domainCommand, CancellationToken cancellationToken)
        {
            return mediator.Send(domainCommand, cancellationToken);
        }

        public Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> domainCommand, CancellationToken cancellationToken)
        {
            return mediator.Send(domainCommand, cancellationToken);
        }
    }
    public static class DomainMessageHandlerDIExtension
    {
        public static IServiceCollection AddDomainMessageHandler(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.TryAddTransient<IDomainMessageHandler, DomainMessageHandler>();
            return services;
        }
    }
}
