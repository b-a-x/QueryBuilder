using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

/*public class TableTranslator : Translator//: CommandTranslator
{
    protected readonly TableBuilder _table;
    protected readonly string command;
    public TableTranslator(string command, TableBuilder table) //: base(command) 
    {
        _table = table;
        this.command = command;
    }

    public override void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(_table.Alias) == false)
        {
            if (string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(command).Append(" ").Append(_table.TableName).Append(" as ").Append(_table.Alias);
            else
                source.Query.Append(command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName).Append(" as ").Append(_table.Alias);
        }
        else
        {
            if(string.IsNullOrEmpty(_table.Schema))
                source.Query.Append(command).Append(" ").Append(_table.TableName);
            else
                source.Query.Append(command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName);
        }
    }

    public static TableTranslator Make(string command, TableBuilder table) => 
        new TableTranslator(command, table);
}

public class TableTranslator<T> : TableTranslator
    where T : ITableBuilder
{
    public TableTranslator(string command) : base(command, T.GetTable()) { }

    public new static TableTranslator<T> Make(string command) =>
        new TableTranslator<T>(command);
}*/

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