using QueryBuilder.Core.Context;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct ElseTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" else ");
    }
}