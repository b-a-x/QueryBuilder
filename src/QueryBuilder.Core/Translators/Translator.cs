using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public abstract class Translator
{
    public abstract void Run(QueryBuilderSource source);
}