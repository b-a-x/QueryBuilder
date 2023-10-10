using QueryBuilder.Core.Queris;
using System.Diagnostics.CodeAnalysis;

namespace QueryBuilder.Core.Translators;

public class EqualToTranslator : Translator
{
    private string _columnName;
    private object _value;

    public EqualToTranslator(string columnName, object value)
    {
        _value = value;
        _columnName = columnName;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append(_columnName).Append(" = @").Append(name);
    }

    public static EqualToTranslator Make([NotNull] string columnName, [NotNull] object value)
    {
        return new EqualToTranslator(columnName, value);
    }
}