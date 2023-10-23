using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNotNullTranslator
{
    private readonly string _column;
    private readonly TableBuilder _table;
    public IsNotNullTranslator(string column, TableBuilder table)
    {
        _column = column;
        _table = table;
    }

    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(_table.Alias).Append(".").Append(_column).Append(" is not null");
    }
}
