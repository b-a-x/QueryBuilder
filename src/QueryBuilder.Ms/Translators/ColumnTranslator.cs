using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct ColumnTranslator
{
    private readonly string _field;
    private readonly Table _table;

    public ColumnTranslator(string field, Table table)
    {
        _field = field;
        _table = table;
    }

    public void Run(QueryBuilderContext source)
    {
        if (string.IsNullOrEmpty(_table.Name))
            throw new Exception("not used interface");

        source.Query.Append(_table.Alias).Append(".").Append(_field).Append(" ");
    }
}