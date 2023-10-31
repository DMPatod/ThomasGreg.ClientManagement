using ClientManagement.Application.Clients.Commands;
using FluentValidation;

namespace ClientManagement.Application.Clients.Validators
{
    public class ClientCreateCommandValidator : AbstractValidator<ClientCreateCommand>
    {
        public ClientCreateCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Logo).NotEmpty();
            
        }
    }
}
