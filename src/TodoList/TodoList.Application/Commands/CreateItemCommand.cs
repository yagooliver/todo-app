using MediatR;

namespace TodoList.Application.Commands
{
    public class CreateItemCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime EndDate { get; set; }
        public bool ShowAlerts { get; set; }
        public Guid UserId { get; set; }
    }
}