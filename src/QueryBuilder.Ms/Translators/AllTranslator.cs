using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct AllTranslator
{
    private readonly Table _table;
    public AllTranslator(Table table)
    {
        _table = table;
    }

    public void Run(QueryBuilderContext source)
    {
        if (string.IsNullOrEmpty(_table.Name))
            throw new Exception("not used interface");

        source.Query.Append(_table.Alias).Append(".* ");
    }
}