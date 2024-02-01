using MediatR;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Handler
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : class
        {
            return mediator.Publish(@event);
        }
        
        public async Task<TResult> Send<TResult>(IRequest<TResult> command)
        {
            return await mediator.Send(command);
        }
    }
}