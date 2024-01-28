using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct FromTranslator
{
    private readonly Table _table;
    public FromTranslator(Table table)
    {
        _table = table;
    }

    public void BeginRun(QBContext source)
    {
        source.Query.Append("\r\n").Append("from (");
    }

    public void EndRun(QBContext source)
    {
        source.Query.Append("\r\n").Append(") ").Append(_table.Alias);
    }
}