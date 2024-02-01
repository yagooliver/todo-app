using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infra.Data.PostgresSQL.Context;

namespace TodoList.Infra.Data.PostgresSQL.Repositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(TodoListDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddNewHistory(ItemHistory itemHistory)
        {
            await _dbContext.Set<ItemHistory>().AddAsync(itemHistory);

            await _dbContext.SaveChangesAsync();
        }


        public async override Task<Item> GetById(Guid id)
        {
            Log.Information("getting item with entry", id);
            return await _dbSet
                .Include(e => e.ItemHistories)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}