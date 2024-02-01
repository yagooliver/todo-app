using AutoMapper;
using MediatR;
using TodoList.Application.Commands;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.CommandHandlers
{
    public class GetItemByIdCommandHandler : IRequestHandler<GetItemByIdCommand, ItemDto>
    {
        private readonly IItemRepository itemRepository;
        private readonly IMapper mapper;

        public GetItemByIdCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            this.itemRepository = itemRepository;
            this.mapper = mapper;
        }


        public async Task<ItemDto> Handle(GetItemByIdCommand request, CancellationToken cancellationToken)
        {
            Item item = await itemRepository.GetById(request.Id);
            return mapper.Map<ItemDto>(item);
        }
    }
}