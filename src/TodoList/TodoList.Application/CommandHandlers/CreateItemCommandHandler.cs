using AutoMapper;
using MediatR;
using Serilog;
using TodoList.Application.Commands;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.CommandHandlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IItemRepository itemRepository;
        private readonly IMapper mapper;

        public CreateItemCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            this.itemRepository = itemRepository;
            this.mapper = mapper;

        }


        public async Task Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"CreateItemCommand - Adding new item with {request.StatusId} Status");
            ArgumentNullException.ThrowIfNull(request);

            await itemRepository.Add(mapper.Map<Item>(request));
        }
    }
}