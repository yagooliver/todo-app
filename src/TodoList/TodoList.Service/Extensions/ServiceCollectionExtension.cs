using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using TodoList.Application.CommandHandlers;
using TodoList.Application.Commands;
using TodoList.Application.Handler;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Models.DTOs;
using TodoList.Infra.Data.PostgresSQL.Context;
using TodoList.Infra.Data.PostgresSQL.Repositories;

namespace TodoList.Service
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Inject the repositories
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepositories(this IServiceCollection services)
        { 
            services.AddScoped<TodoListDbContext>();
            services.AddScoped<IItemRepository, ItemRepository>();
        }

        /// <summary>
        /// inject the command handlers
        /// </summary>
        /// <param name="services"></param>
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Add requests
            services.AddScoped<IRequestHandler<CreateItemCommand>, CreateItemCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateItemCommand>, UpdateItemCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveItemCommand>, RemoveItemCommandHandler>();
            services.AddScoped<IRequestHandler<GetItemByIdCommand, ItemDto>, GetItemByIdCommandHandler>();
            services.AddScoped<IRequestHandler<GetItemBySearchCommand, List<ItemDto>>, GetItemBySearchCommandHandler>();
        }

        public static string Sha256(this string input)
        {

            using SHA256 sHA = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(sHA.ComputeHash(bytes));
        }

        public static string GetDisplayName(this ClaimsPrincipal principal)
        {
            string name = principal?.Identity?.Name;
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            Claim claim = principal?.FindFirst("id");
            if (claim != null)
            {
                return claim.Value;
            }

            return string.Empty;
        }
    }

    
}