using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Core.Translators;

public interface IWhereTranslator<T>
{
    IWhereTranslator<T> EqualTo<TField>(Expression<Func<T, TField>> column, TField value);
}

public class WhereTranslator<T> : CommandTranslator, IWhereTranslator<T>
{
    public WhereTranslator(string command) : base(command) { }

    public WhereTranslator<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        //new EqualToTranslator<T>(CommonExpression.GetColumnName(column), value).Run(source);
        return this;
    }

    public override void Run(QueryBuilderSource source)
    {
    }

    IWhereTranslator<T> IWhereTranslator<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value)
    {
        EqualTo(column, value);
        return this;
    }

    public static WhereTranslator<T> Make(Action<WhereTranslator<T>> inner)
    {
        var obj = new WhereTranslator<T>("where");
        inner?.Invoke(obj);
        return obj;
    }
}
