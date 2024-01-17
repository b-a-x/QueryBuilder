using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNullTranslator
{
    public void Run(QueryBuilderContext source)
    {
        source.Query.Append("is null");
    }
}
