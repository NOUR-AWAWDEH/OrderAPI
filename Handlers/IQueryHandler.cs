namespace OrdersAPI.Handlers;

public interface IQueryHandler<TQuery, TResult> where TQuery : notnull
{
    Task<TResult> HandleAsync(TQuery query);
}
