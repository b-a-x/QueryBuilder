using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct OrTranslator
{
    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(" or ");
    }
}