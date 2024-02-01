using MediatR;

namespace TodoList.Application.Commands
{
    public class RemoveItemCommand : IRequest
    {
        public Guid Id {get;set;}
    }
}