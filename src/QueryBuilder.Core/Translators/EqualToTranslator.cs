using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct EqualToTranslator
{
    private readonly string _column;
    private readonly object _value;
    private readonly TableBuilder _table;

    public EqualToTranslator(string column, object value, TableBuilder table)
    {
        _column = column;
        _value = value;
        _table = table;
    }

    public void Run(QueryBuilderSource source)
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append(_table.Alias).Append(".").Append(_column).Append(" = @").Append(name);
    }
}

public readonly ref struct EqualTranslator
{
    private readonly string _columnLeft;
    private readonly string _columnRigth;
    private readonly TableBuilder _tableLeft;
    private readonly TableBuilder _tableRigth;
    public EqualTranslator(string columnLeft, string columnRigth, TableBuilder tableLeft, TableBuilder tableRigth)
    {
        _columnLeft = columnLeft;
        _columnRigth = columnRigth;
        _tableLeft = tableLeft;
        _tableRigth = tableRigth;
    }

    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(_tableLeft.Alias).Append(".").Append(_columnLeft).Append(" = ").Append(_tableRigth.Alias).Append(".").Append(_columnRigth);
    }
}