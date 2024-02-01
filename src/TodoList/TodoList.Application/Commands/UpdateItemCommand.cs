using MediatR;

namespace TodoList.Application.Commands
{
    public class UpdateItemCommand : IRequest
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime EndDate { get; set; }
        public string Comments { get; set; }
    }
}