using MediatR;

namespace BuildBlocks.Core.Messages
{
    public interface ICommand : ICommand<bool>
    {
    }

    public interface ICommand<out T> : IRequest<T>
    {
    }
}
