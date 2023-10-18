using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct AndTranslator
{
    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(" and ");
    }
}