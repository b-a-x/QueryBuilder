using QueryBuilder.Core.Context;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNullTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append("is null");
    }
}
