using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct OrTranslator
{
    public void Run(QueryBuilderContext source)
    {
        source.Query.Append(" or ");
    }
}