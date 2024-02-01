using MediatR;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.Commands
{
    public class GetItemBySearchCommand : IRequest<List<ItemDto>>
    {
        
    }
}