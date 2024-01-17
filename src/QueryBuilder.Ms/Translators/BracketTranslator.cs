using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct BracketTranslator
{
    public void BeforeRun(QueryBuilderContext source)
    {
        source.Query.Append("(");
    }

    public void AfterRun(QueryBuilderContext source) 
    {
        source.Query.Append(")");
    }
}
