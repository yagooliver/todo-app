using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface IItemRepository : IRepositoryBase<Item>
    {
         Task AddNewHistory(ItemHistory itemHistory);
    }
}