using AutoMapper;
using MediatR;
using Serilog;
using TodoList.Application.Commands;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.CommandHandlers
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IItemRepository itemRepository;
        private readonly IMapper mapper;

        public UpdateItemCommandHandler(IItemRepository itemRepository, IMapper mapper)
        {
            this.itemRepository = itemRepository;
            this.mapper = mapper;

        }
        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"UpdateItemCommand - Updating {request.Name} item with {request.StatusId} Status");
            
            ArgumentNullException.ThrowIfNull(request);
            Item item = await itemRepository.GetById(request.Id);
            int oldStatus = (int)item.StatusId;
            Log.Information("Item ", item);
            mapper.Map(request, item);
            await itemRepository.Update(item);
            
            if(oldStatus != item.StatusId)
            {
                ItemHistory itemHistory = new ItemHistory(item.Id, (ItemStatusEnum)request.StatusId, (ItemStatusEnum)oldStatus);
                await itemRepository.AddNewHistory(itemHistory); 
            }
            
        }
    }
}