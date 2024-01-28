using QueryBuilder.Core.Context;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct AsTranslator
{
    private readonly string _value;
    public AsTranslator(string value)
    {
        _value = value;
    }

    public void Run(QBContext source)
    {
        source.Query.Append("as ").Append(_value).Append(" ");
    }
}