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
        private readonly IHttpContextAccessor httpContextAccessor;

        public ItemController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;

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

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            
            // AuthenticateResult result = await HttpContext.AuthenticateAsync();
            // IEnumerable<string> Clients = null;
            // if (result.Properties.Items.ContainsKey("client_list"))
            // {
            //     var encoded = result.Properties.Items["client_list"];
            //     var bytes = Decode(encoded);
            //     var value = Encoding.UTF8.GetString(bytes);

            //     Clients = JsonSerializer.Deserialize<string[]>(value);
            // }

            return Ok(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));//.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        //
    // Summary:
    //     Encodes the specified byte array.
    //
    // Parameters:
    //   arg:
    //     The argument.
    public static string Encode(byte[] arg)
    {
        return Convert.ToBase64String(arg).Split('=')[0].Replace('+', '-').Replace('/', '_');
    }

    //
    // Summary:
    //     Decodes the specified string.
    //
    // Parameters:
    //   arg:
    //     The argument.
    //
    // Exceptions:
    //   T:System.Exception:
    //     Illegal base64url string!
    public static byte[] Decode(string arg)
    {
        string text = arg;
        text = text.Replace('-', '+');
        text = text.Replace('_', '/');
        switch (text.Length % 4)
        {
            case 2:
                text += "==";
                break;
            case 3:
                text += "=";
                break;
            default:
                throw new Exception("Illegal base64url string!");
            case 0:
                break;
        }

        return Convert.FromBase64String(text);
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