using MediatR;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.Commands
{
    public class GetItemByIdCommand : IRequest<ItemDto>
    {
        public Guid Id {get;}

        public GetItemByIdCommand(Guid id)
        {
            Id = id;
        }

    }
}