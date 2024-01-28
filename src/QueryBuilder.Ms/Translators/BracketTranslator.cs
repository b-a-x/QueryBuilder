using QueryBuilder.Core.Context;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct BracketTranslator
{
    public void BeforeRun(QBContext source)
    {
        source.Query.Append("(");
    }

    public void AfterRun(QBContext source) 
    {
        source.Query.Append(")");
    }
}
