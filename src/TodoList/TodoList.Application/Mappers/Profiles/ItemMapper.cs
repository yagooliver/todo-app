using AutoMapper;
using TodoList.Application.Commands;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Application.Mappers.Profiles
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            CreateMap<CreateItemCommand, Item>()
                .ForMember(e => e.ItemHistories, opt => opt.MapFrom(y => ResolveCreateHistory(y)));

            CreateMap<UpdateItemCommand, Item>()
                .ForMember(e => e.Id, opt => opt.Ignore())
                .ForMember(e => e.ItemHistories, opt => opt.Ignore())
                .ForMember(e => e.CreatedAt, opt => opt.Ignore());
            CreateMap<Item, ItemDto>();
        }

        private List<ItemHistory> ResolveCreateHistory(CreateItemCommand command)
        {
            return new List<ItemHistory>
            {
                new ItemHistory((ItemStatusEnum)command.StatusId)
            };
        }
    }
}