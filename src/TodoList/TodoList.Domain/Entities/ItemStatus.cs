using TodoList.Domain.Enums;

namespace TodoList.Domain.Entities
{
    public class ItemStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemStatusEnum Status { get; set; }

        public IList<Item> Items { get; set; }
    }
}
