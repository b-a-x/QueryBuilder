using QueryBuilder.Core.Queris;

namespace QueryBuilder.Core.Translators;

public interface ITableTranslator<T>
{
    ITableTranslator<T> WithTable(string table);
    ITableTranslator<T> WithSchema(string schema);
    ITableTranslator<T> WithAlias(string alias);
}

public class TableTranslator<T> : CommandTranslator , ITableTranslator<T>
{
    private string _alias;
    private string _table;
    private string _schema;
    public TableTranslator(string command, string schema) : base(command) 
    { 
        _schema = schema; 
    }

    public override void Run(QueryBuilderSource source)
    {
        var table = string.IsNullOrWhiteSpace(_table) ? typeof(T).Name : _table;
        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_alias) == false)
        {
            if (string.IsNullOrEmpty(_schema))
                source.Query.Append(command).Append(" ").Append(table).Append(" as ").Append(_alias);
            else
                source.Query.Append(command).Append(" ").Append(_schema).Append(".").Append(table).Append(" as ").Append(_alias);
        }
        else
        {
            if(string.IsNullOrEmpty(_schema))
                source.Query.Append(command).Append(" ").Append(table);
            else
                source.Query.Append(command).Append(" ").Append(_schema).Append(".").Append(table);
        }
    }

    public TableTranslator<T> WithTable(string table)
    {
        _table = table;
        return this;
    }

    public TableTranslator<T> WithSchema(string schema)
    {
        _schema = schema;
        return this;
    }
    public TableTranslator<T> WithAlias(string alias)
    {
        _alias = alias;
        return this;
    }

    ITableTranslator<T> ITableTranslator<T>.WithTable(string table)
    {
        return WithTable(table);
    }

    ITableTranslator<T> ITableTranslator<T>.WithSchema(string schema)
    {
        return WithSchema(schema);
    }

    ITableTranslator<T> ITableTranslator<T>.WithAlias(string alias)
    {
        return WithAlias(alias);
    }

    public static TableTranslator<T> Make(string command, Action<TableTranslator<T>> inner)
    {
        var obj = new TableTranslator<T>(command, "");
        inner?.Invoke(obj);
        return obj;
    }
}
