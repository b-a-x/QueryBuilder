using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Translators;

public class AllTranslator : Translator
{
    protected readonly TableBuilder _table;
    public AllTranslator(TableBuilder table)
    {
        _table = table;
    }

    public override void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        source.Query.Append(_table.Alias).Append(".* ");
    }

    /*public static AllTranslator Make()
    {
        return new AllTranslator();
    }*/
}

public class AllTranslator<T> : AllTranslator
    where T : ITableBuilder
{
    public AllTranslator() : base(T.GetTable())
    {
    }

    public static AllTranslator<T> Make()
    {
        return new AllTranslator<T>();
    }
}
