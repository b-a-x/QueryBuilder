using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public class JoinTranslator<T> : TableTranslator<T>
    where T : ITableBuilder
{
    public JoinTranslator(string command) : base(command)
    {
    }

    public override void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append("\r\n").Append(command).Append(" ").Append(_table.Schema).Append(".").Append(_table.TableName).Append(" as ").Append(_table.Alias).Append(" on ");
    }

    public new static JoinTranslator<T> Make(string command) =>
        new JoinTranslator<T>(command);
}
