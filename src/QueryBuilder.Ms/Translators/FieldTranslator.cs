using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct FieldTranslator
{
    private readonly string _field;
    private readonly TableBuilder _table;

    public FieldTranslator(string field, TableBuilder table)
    {
        _field = field;
        _table = table;
    }

    public void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        if (source.Query[source.Query.Length - 7] != 's' && source.Query[source.Query.Length - 2] != 't')
            source.Query.Append(",");

        source.Query.Append(_table.Alias).Append(".").Append(_field).Append(" ");
    }
}

/*
public class FieldTranslator : Translator
{
    protected readonly string _fieldName;
    protected readonly TableBuilder _table;
    public FieldTranslator(string fieldName, TableBuilder table)
    {
        _fieldName = fieldName;
        _table = table;
    }

    public override void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        if (source.Query[source.Query.Length - 7] != 's' && source.Query[source.Query.Length - 2] != 't')
            source.Query.Append(",");

        source.Query.Append(_table.Alias).Append(".").Append(_fieldName).Append(" ");
    }
}

public class FieldTranslator<T> : FieldTranslator
    where T : ITableBuilder
{
    public FieldTranslator(string fieldName) : base(fieldName, T.GetTable())
    {
    }

    public static FieldTranslator<T> Make(string fieldName) =>
        new FieldTranslator<T>(fieldName);
}*/