using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct ColumnTranslator
{
    private readonly string _field;
    private readonly TableBuilder _table;

    public ColumnTranslator(string field, TableBuilder table)
    {
        _field = field;
        _table = table;
    }

    public void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append(_table.Alias).Append(".").Append(_field).Append(" ");
    }
}