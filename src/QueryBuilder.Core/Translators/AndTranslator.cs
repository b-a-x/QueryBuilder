using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Translators;

public readonly ref struct AndTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" and ");
    }
}