using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;

namespace TodoList.Infra.Data.PostgresSQL.Context
{
    public class DbInitializer
    {
        public static void SeedData(TodoListDbContext context)
        {
            context.Database.Migrate();

            if (context.ItemStatuses.Any())
            {
                Console.WriteLine("Already have data - no need to seed");
                return;
            }

            List<ItemStatus> itemStatuses = CreateItemStatusList();
            
            context.ItemStatuses.AddRange(itemStatuses);

            context.SaveChanges();
        }

        
        private static List<ItemStatus> CreateItemStatusList()
        {
            var itemStatusList = new List<ItemStatus>();

            foreach (ItemStatusEnum statusEnum in Enum.GetValues(typeof(ItemStatusEnum)))
            {
                var itemStatus = new ItemStatus
                {
                    Id = (int)statusEnum,
                    Name = Enum.GetName(typeof(ItemStatusEnum), statusEnum),
                    Description = GetDescription(statusEnum),
                    Status = statusEnum
                };

                itemStatusList.Add(itemStatus);
            }

            return itemStatusList;
        }

        private static string GetDescription(ItemStatusEnum statusEnum)
        {
            // Add descriptions for each status if needed
            switch (statusEnum)
            {
                case ItemStatusEnum.New:
                    return "New item";
                case ItemStatusEnum.InProgress:
                    return "Item in progress";
                case ItemStatusEnum.Done:
                    return "Item completed";
                case ItemStatusEnum.OnHold:
                    return "Item on hold";
                case ItemStatusEnum.Blocked:
                    return "Item blocked";
                case ItemStatusEnum.Deleted:
                    return "Item deleted";
                default:
                    return string.Empty;
            }
        }
    }
}