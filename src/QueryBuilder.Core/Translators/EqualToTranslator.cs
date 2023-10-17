using QueryBuilder.Core.Queries;
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

public class EqualTranslator : Translator
{
    private readonly string _columnNameLeft;
    private readonly string _columnNameRigth;

    public EqualTranslator(string columnNameLeft, string columnNameRigth)
    {
        _columnNameLeft = columnNameLeft;
        _columnNameRigth = columnNameRigth;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append(_columnNameLeft).Append(" = ").Append(_columnNameRigth);
    }

    public static EqualTranslator Make([NotNull] string columnNameLeft, [NotNull] string columnNameRigth)
    {
        return new EqualTranslator(columnNameLeft, columnNameRigth);
    }
}