using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Translators;

public class AsTranslator : Translator
{
    private readonly string _value;
    public AsTranslator(string value)
    {
        _value = value;
    }
    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append("as ").Append(_value).Append(" ");
    }

    public static AsTranslator Make(string value)
    {
        return new AsTranslator(value);
    }
}
