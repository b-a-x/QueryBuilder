using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

/*public class JoinTranslator<T> : Translator//: TableTranslator<T>
    where T : ITableBuilder
{
    private readonly string command;
    public JoinTranslator(string command) //: base(command)
    {
        this.command = command;
    }

    public override void Run(QueryBuilderSource source)
    {
        var table = T.GetTable();
        if (string.IsNullOrEmpty(table.TableName))
            throw new Exception("not used interface");

        source.Query.Append("\r\n").Append(command).Append(" ").Append(table.Schema).Append(".").Append(table.TableName).Append(" as ").Append(table.Alias).Append(" on ");
    }

    public new static JoinTranslator<T> Make(string command) =>
        new JoinTranslator<T>(command);
}
*/

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