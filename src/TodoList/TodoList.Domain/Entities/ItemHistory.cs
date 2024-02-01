using TodoList.Domain.Enums;

namespace TodoList.Domain.Entities
{
    public class ItemHistory : EntityBase
    {
        public ItemHistory(ItemStatusEnum newStatus)
        {
            NewStatus = newStatus;
        }

        public ItemHistory(Guid itemId, ItemStatusEnum newStatus, ItemStatusEnum? oldStatus)
        {
            ItemId = itemId;
            NewStatus = newStatus;
            OldStatus = oldStatus;
        }

        public Guid ItemId {get;set;}
        public ItemStatusEnum NewStatus {get;set;}
        public ItemStatusEnum? OldStatus { get; set; }

        public virtual Item Item{get;set;}
    }
}