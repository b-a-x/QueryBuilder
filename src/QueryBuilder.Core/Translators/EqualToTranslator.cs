using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

//TODO: Убрать object ?
public readonly ref struct EqualToTranslator
{
    private readonly string _column;
    private readonly object _value;
    private readonly Table _table;

    public EqualToTranslator(string column, object value, Table table)
    {
        _column = column;
        _value = value;
        _table = table;
    }

    public void Run(QueryBuilderContext source)
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append(_table.Alias).Append(".").Append(_column).Append(" = @").Append(name);
    }
}

public readonly ref struct EqualTranslator
{
    private readonly string _columnLeft;
    private readonly string _columnRigth;
    private readonly Table _tableLeft;
    private readonly Table _tableRigth;
    public EqualTranslator(string columnLeft, string columnRigth, Table tableLeft, Table tableRigth)
    {
        _columnLeft = columnLeft;
        _columnRigth = columnRigth;
        _tableLeft = tableLeft;
        _tableRigth = tableRigth;
    }

    public void Run(QueryBuilderContext source)
    {
        source.Query.Append(_tableLeft.Alias).Append(".").Append(_columnLeft).Append(" = ").Append(_tableRigth.Alias).Append(".").Append(_columnRigth);
    }
}