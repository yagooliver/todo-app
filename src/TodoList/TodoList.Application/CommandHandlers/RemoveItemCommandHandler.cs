using MediatR;
using TodoList.Application.Commands;

namespace TodoList.Application.CommandHandlers
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand>
    {
        public Task Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}