namespace QueryBuilder.Core.Queries;

public class QueryBuilderCore
{
    public readonly QueryBuilderContext Context;

    protected QueryBuilderCore(QueryBuilderContext context)
    {
        Context = context;
    }

    public static TResult Make<TResult>(QueryBuilderContext source, Action<TResult>? inner = null)
        where TResult : class, new()
    {
        var obj = new TResult(source);
        inner?.Invoke(obj);
        return obj;
    }
}
