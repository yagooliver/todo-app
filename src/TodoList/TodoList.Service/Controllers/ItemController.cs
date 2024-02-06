using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TodoList.Application.Commands;
using TodoList.Domain.Models.DTOs;

namespace TodoList.Service.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator mediator;

        public ItemController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]CreateItemCommand createItemCommand)
        {
            await mediator.Send(createItemCommand);
            return new CreatedResult();
        }

        [HttpGet]
        [Authorize]
        public async Task<List<ItemDto>> Get()
        {
            return await mediator.Send(new GetItemBySearchCommand());
        }

        [HttpGet("{id}")]
        public async Task<ItemDto> Get(Guid id)
        {
            return await mediator.Send(new GetItemByIdCommand(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UpdateItemCommand updateItemCommand)
        {
            await mediator.Send(updateItemCommand);
            return new CreatedResult();
        }
    }
}