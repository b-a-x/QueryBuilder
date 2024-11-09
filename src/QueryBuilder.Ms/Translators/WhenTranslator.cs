using QueryBuilder.Core.Context;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct WhenTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" when ");
    }
}
