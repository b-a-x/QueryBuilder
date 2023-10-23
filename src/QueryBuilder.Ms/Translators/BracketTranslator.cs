using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct BracketTranslator
{
    public void BeforeRun(QueryBuilderSource source)
    {
        source.Query.Append("(");
    }

    public void AfterRun(QueryBuilderSource source) 
    {
        source.Query.Append(")");
    }
}
