using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infra.Data.PostgresSQL.Mappings;

namespace TodoList.Infra.Data.PostgresSQL.Context
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStatus> ItemStatuses { get; set; }
        public DbSet<ItemHistory> ItemHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemMapping());
            modelBuilder.ApplyConfiguration(new ItemHistoryMapping());
            modelBuilder.ApplyConfiguration(new ItemStatusMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
