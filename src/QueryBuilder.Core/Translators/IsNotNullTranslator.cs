using QueryBuilder.Core.Context;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Core.Translators;

public readonly ref struct IsNotNullTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" is not null");
    }
}
