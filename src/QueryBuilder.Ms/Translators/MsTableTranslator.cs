using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Translators;

public interface IMsTableTranslator<T>
{
    IMsTableTranslator<T> WithTable(string table);
    IMsTableTranslator<T> WithSchema(string schema);
    IMsTableTranslator<T> WithAlias(string alias);
}

public class MsTableTranslator<T> : TableTranslator<T>, IMsTableTranslator<T>
{
    public MsTableTranslator(string command, string schema = "dbo") : base(command, schema) { }

    IMsTableTranslator<T> IMsTableTranslator<T>.WithAlias(string alias)
    {
        return WithAlias(alias) as MsTableTranslator<T>;
    }

    IMsTableTranslator<T> IMsTableTranslator<T>.WithSchema(string schema)
    {
        return WithSchema(schema) as MsTableTranslator<T>;
    }

    IMsTableTranslator<T> IMsTableTranslator<T>.WithTable(string table)
    {
        return WithTable(table) as MsTableTranslator<T>;
    }

    public static MsTableTranslator<T> Make(string command, Action<MsTableTranslator<T>> inner)
    {
        var obj = new MsTableTranslator<T>(command);
        inner?.Invoke(obj);
        return obj;
    }
}
