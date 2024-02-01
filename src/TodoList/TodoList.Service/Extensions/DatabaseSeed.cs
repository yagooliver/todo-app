using TodoList.Infra.Data.PostgresSQL.Context;

namespace TodoList.Service 
{
    public static class DatabaseSeed
    {
        public static void SeedDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                DbInitializer.SeedData(scope.ServiceProvider.GetRequiredService<TodoListDbContext>());
            }
        }
    }
}