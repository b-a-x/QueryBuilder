using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Translators;

public class AllTranslator : Translator
{
    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append("* ");
    }

    public static AllTranslator Make()
    {
        return new AllTranslator();
    }
}
