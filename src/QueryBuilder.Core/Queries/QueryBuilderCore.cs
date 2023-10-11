namespace QueryBuilder.Core.Queries;

public class QueryBuilderCore
{
    public readonly QueryBuilderSource Source;

    protected QueryBuilderCore(QueryBuilderSource source)
    {
        Source = source;
    }
}
