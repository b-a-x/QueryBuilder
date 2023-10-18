using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct JoinTranslator
{
    private readonly TableBuilder _table;
    private readonly string _command;
    public JoinTranslator(string command, TableBuilder table)
    {
        _table = table;
        _command = command;
    }
    public void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append("\r\n").Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName).Append(" as ").Append(_table.Alias).Append(" on ");
    }
}