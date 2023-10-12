
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Helpers;

namespace QueryBuilder.Ms.Queries;

public class MsTableTranslator<T> : TableTranslator
    where T : IMsTableTranslator
{
    public MsTableTranslator(string command) : base(command, T.Schema, T.Table, T.Alias) 
    { 
    }

    /*public override void Run(QueryBuilderSource source)
    {
        var schema = T.Schema;
        var table = T.Table;
        if (string.IsNullOrEmpty(T.Table))
            throw new Exception("not used interface");

        source.Query.Append("\r\n");
        if (string.IsNullOrEmpty(T.Alias) == false)
        {
            if (string.IsNullOrEmpty(T.Schema))
                source.Query.Append(command).Append(" ").Append(T.Table).Append(" as ").Append(T.Alias);
            else
                source.Query.Append(command).Append(" ").Append(T.Schema).Append(".").Append(T.Table).Append(" as ").Append(T.Alias);
        }
        else
        {
            if (string.IsNullOrEmpty(T.Schema))
                source.Query.Append(command).Append(" ").Append(T.Table);
            else
                source.Query.Append(command).Append(" ").Append(T.Schema).Append(".").Append(T.Table);
        }
    }*/

    public static MsTableTranslator<T> Make(string command) =>
        new MsTableTranslator<T>(command);
}
