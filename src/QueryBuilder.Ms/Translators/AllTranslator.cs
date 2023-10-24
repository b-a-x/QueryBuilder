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

        source.Query.Append(_table.Alias).Append(".* ");
    }
}