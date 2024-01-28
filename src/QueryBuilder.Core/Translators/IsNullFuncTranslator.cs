using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNullFuncTranslator
{
    private readonly object _value;
    private readonly string _column;
    private readonly Table _table;

    public IsNullFuncTranslator(string column, object value, Table table)
    {
        _column = column;
        _value = value;
        _table = table;
    }

    public void Run(QBContext source)
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append("isnull(")
                    .Append(_table.Alias)
                    .Append(".")
                    .Append(_column)
                    .Append(", @")
                    .Append(name)
                    .Append(") ");
    }
}
