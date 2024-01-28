using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Translators;

public readonly ref struct OrTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" or ");
    }
}