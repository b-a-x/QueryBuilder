using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNullTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append("is null");
    }
}