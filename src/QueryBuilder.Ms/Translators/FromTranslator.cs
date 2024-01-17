using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct FromTranslator
{
    private readonly Table _table;
    public FromTranslator(Table table)
    {
        _table = table;
    }

    public void BeginRun(QueryBuilderContext source)
    {
        source.Query.Append("\r\n").Append("from (");
    }

    public void EndRun(QueryBuilderContext source)
    {
        source.Query.Append("\r\n").Append(") ").Append(_table.Alias);
    }
}