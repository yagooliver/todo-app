using AutoMapper;
using MediatR;
using TodoList.Application.Commands;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.CommandHandlers
{
    public class GetItemBySearchCommandHandler : IRequestHandler<GetItemBySearchCommand, List<ItemDto>>
    {
        private readonly IItemRepository itemRepository;
        private readonly IMapper mapper;

        public GetItemBySearchCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            this.itemRepository = itemRepository;
            this.mapper = mapper;
        }
        public async Task<List<ItemDto>> Handle(GetItemBySearchCommand request, CancellationToken cancellationToken)
        {
            var items = await itemRepository.GetAll();
            return mapper.Map<List<ItemDto>>(items);
        }

    }
}