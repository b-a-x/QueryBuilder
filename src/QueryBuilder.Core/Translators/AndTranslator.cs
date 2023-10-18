using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

/*public class AndTranslator : Translator
{
    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append(" and ");
    }

    public static AndTranslator Make()
    {
        return new AndTranslator();
    }
}
*/

public readonly ref struct AndTranslator
{
    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(" and ");
    }
}