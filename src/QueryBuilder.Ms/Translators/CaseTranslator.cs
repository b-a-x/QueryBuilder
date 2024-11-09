using QueryBuilder.Core.Context;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct CaseTranslator
{
    public void BeforeRun(QBContext source)
    {
        source.Query.Append("case");
    }

    public void AfterRun(QBContext source)
    {
        source.Query.Append(" end ");
    }
}
