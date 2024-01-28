using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Core.Translators;

public readonly ref struct JoinTranslator
{
    private readonly Table _table;
    private readonly string _command;
    public JoinTranslator(string command, Table table)
    {
        _table = table;
        _command = command;
    }
    public void Run(QBContext source)
    {
        if (string.IsNullOrEmpty(_table.Name))
            throw new Exception("not used interface");

        source.Query.Append("\r\n").Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.Name).Append(" as ").Append(_table.Alias).Append(" on ");
    }
}