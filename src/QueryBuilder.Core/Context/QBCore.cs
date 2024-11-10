namespace QueryBuilder.Core.Context;

public class QBCore
{
    protected QBContext context { get; set; }

    public static TResult Make<TResult>(out QBContext context, Action<TResult>? inner = null)
        where TResult : QBCore, new()
    {
        context = new QBContext();
        return Make(context, inner);
    }

    public static TResult Make<TResult>(QBContext context, Action<TResult>? inner = null)
        where TResult : QBCore, new()
    {
        var result = new TResult
        {
            context = context
        };
        inner?.Invoke(result);
        return result;
    }
}
