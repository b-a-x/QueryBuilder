using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct TableTranslator
{
    private readonly TableBuilder _table;
    private readonly string _command;

    public TableTranslator(string command, TableBuilder table)
    {
        _table = table;
        _command = command;
    }

    public void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_table.Alias) == false)
        {
            if (string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(_command).Append(" ").Append(_table.TableName).Append(" as ").Append(_table.Alias);
            else
                source.Query.Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName).Append(" as ").Append(_table.Alias);
        }
        else
        {
            if (string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(_command).Append(" ").Append(_table.TableName);
            else
                source.Query.Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName);
        }
    }
}