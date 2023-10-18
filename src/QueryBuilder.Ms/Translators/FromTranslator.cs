using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct FromTranslator
{
    private readonly TableBuilder _table;
    public FromTranslator(TableBuilder table)
    {
        _table = table;
    }

    public void BeginRun(QueryBuilderSource source)
    {
        source.Query.Append("\r\n").Append("from (");
    }

    public void EndRun(QueryBuilderSource source)
    {
        source.Query.Append("\r\n").Append(") ").Append(_table.Alias);
    }
}