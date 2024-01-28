using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct AllTranslator
{
    private readonly Table _table;
    public AllTranslator(Table table)
    {
        _table = table;
    }

    public void Run(QBContext source)
    {
        if (string.IsNullOrEmpty(_table.Name))
            throw new Exception("not used interface");

        source.Query.Append(_table.Alias).Append(".* ");
    }
}