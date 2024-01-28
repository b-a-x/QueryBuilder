using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.Core.Translators;

public readonly ref struct TableTranslator
{
    private readonly Table _table;
    private readonly string _command;

    public TableTranslator(string command, Table table)
    {
        _table = table;
        _command = command;
    }

    public void Run([NotNull]QBContext source)
    {
        if (string.IsNullOrEmpty(_table.Name))
            throw new Exception("not used interface");

        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_table.Alias) == false)
        {
            if (string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(_command).Append(" ").Append(_table.Name).Append(" as ").Append(_table.Alias);
            else
                source.Query.Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.Name).Append(" as ").Append(_table.Alias);
        }
        else
        {
            if (string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(_command).Append(" ").Append(_table.Name);
            else
                source.Query.Append(_command).Append(" ").Append(_table.Schema).Append(".").Append(_table.Name);
        }
    }
}