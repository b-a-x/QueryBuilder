using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct AllTranslator
{
    private readonly TableBuilder _table;
    public AllTranslator(TableBuilder table)
    {
        _table = table;
    }

    public void Run(QueryBuilderSource source)
    {
        if (string.IsNullOrEmpty(_table.TableName))
            throw new Exception("not used interface");

        if (source.Query[source.Query.Length - 7] != 's' && source.Query[source.Query.Length - 2] != 't')
            source.Query.Append(",");

        source.Query.Append(_table.Alias).Append(".* ");
    }
}