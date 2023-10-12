using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public class TableTranslator : CommandTranslator
{
    private readonly string _alias;
    private readonly string _table;
    private readonly string _schema;
    public TableTranslator(string command, string schema, string table, string alias) : base(command) 
    { 
        _schema = schema;
        _table = table;
        _alias = alias;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_alias) == false)
        {
            if (string.IsNullOrEmpty(_schema))
                source.Query.Append(command).Append(" ").Append(_table).Append(" as ").Append(_alias);
            else
                source.Query.Append(command).Append(" ").Append(_schema).Append(".").Append(_table).Append(" as ").Append(_alias);
        }
        else
        {
            if(string.IsNullOrEmpty(_schema))
                source.Query.Append(command).Append(" ").Append(_table);
            else
                source.Query.Append(command).Append(" ").Append(_schema).Append(".").Append(_table);
        }
    }

    public static TableTranslator Make(string command, string schema, string table, string alias) => 
        new TableTranslator(command, schema, table, alias);
}