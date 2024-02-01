using MediatR;

namespace TodoList.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task<TResult> Send<TResult>(IRequest<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}