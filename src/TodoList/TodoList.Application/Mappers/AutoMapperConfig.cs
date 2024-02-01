using AutoMapper;
using TodoList.Application.Mappers.Profiles;

namespace TodoList.Application.Mappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ItemMapper());
            });
        }
    }
}