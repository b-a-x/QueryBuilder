namespace QueryBuilder.Core.Queries;

public class QBCore
{
    protected QueryBuilderContext? Context { get; set; }

    public static TResult Make<TResult>(out QueryBuilderContext context, Action<TResult>? inner = null)
        where TResult : QBCore, new()
    {
        context = new QueryBuilderContext();
        return Make(context, inner);
    }

    public static TResult Make<TResult>(QueryBuilderContext context, Action<TResult>? inner = null)
        where TResult : QBCore, new()
    {
        var result = new TResult
        {
            Context = context
        };
        inner?.Invoke(result);
        return result;
    }
}
