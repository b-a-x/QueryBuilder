using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Translators;

public readonly ref struct CommaSelectTranslator
{
    public void Run(QBContext source)
    {
        if (source.Query[source.Query.Length - 7] != 's' || source.Query[source.Query.Length - 2] != 't')
            source.Query.Append(",");
    }
}