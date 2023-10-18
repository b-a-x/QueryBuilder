using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

/*public class EqualToTranslator : Translator
{
    private readonly string _columnName;
    private readonly object _value;
    private readonly TableBuilder _table;

    public EqualToTranslator(string columnName, object value, TableBuilder table)
    {
        _value = value;
        _columnName = columnName;
        _table = table;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append(_table.Alias).Append(".").Append(_columnName).Append(" = @").Append(name);
    }
}

public class EqualToTranslator<T> : EqualToTranslator
    where T : ITableBuilder
{
    public EqualToTranslator(string columnName, object value) : base(columnName, value, T.GetTable())
    {
    }

    public static EqualToTranslator<T> Make(string columnName, object value)
        => new EqualToTranslator<T>(columnName, value);
}*/

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

/*
public class EqualTranslator : Translator
{
    private readonly string _columnNameLeft;
    private readonly string _columnNameRigth;
    private readonly TableBuilder _tableLeft;
    private readonly TableBuilder _tableRigth;

    public EqualTranslator(string columnNameLeft, string columnNameRigth, TableBuilder tableLeft, TableBuilder tableRigth)
    {
        _columnNameLeft = columnNameLeft;
        _columnNameRigth = columnNameRigth;
        _tableLeft = tableLeft;
        _tableRigth = tableRigth;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append(_tableLeft.Alias).Append(".").Append(_columnNameLeft).Append(" = ").Append(_tableRigth.Alias).Append(".").Append(_columnNameRigth);
    }
}

public class EqualTranslator<TLeft, TRigth> : EqualTranslator
    where TLeft : ITableBuilder
    where TRigth : ITableBuilder
{
    public EqualTranslator(string columnNameLeft, string columnNameRigth) : base(columnNameLeft, columnNameRigth, TLeft.GetTable(), TRigth.GetTable())
    {
    }

    public static EqualTranslator<TLeft, TRigth> Make(string columnNameLeft, string columnNameRigt) 
        => new EqualTranslator<TLeft, TRigth>(columnNameLeft, columnNameRigt);
}*/