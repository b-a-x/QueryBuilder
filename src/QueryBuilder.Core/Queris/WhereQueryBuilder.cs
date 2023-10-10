using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Translators;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Core.Queris;

public interface IWhereQueryBuilder<T>
{
    IWhereQueryBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IWhereQueryBuilder<T> And();
}

public class WhereQueryBuilder<T> : QueryBuilderCore, IWhereQueryBuilder<T>
{
    public WhereQueryBuilder(QueryBuilderSource source) : base(source) { }

    public WhereQueryBuilder<T> Where()
    {
        CommandTranslator.Make("where").Run(Source);
        return this;
    }

    public WhereQueryBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        EqualToTranslator.Make(CommonExpression.GetColumnName(column), value).Run(Source);
        return this;
    }

    public WhereQueryBuilder<T> And()
    {
        AndTranslator.Make().Run(Source);
        return this;
    }

    public static WhereQueryBuilder<T> Make(QueryBuilderSource source, Action<WhereQueryBuilder<T>> inner)
    {
        var obj = new WhereQueryBuilder<T>(source).Where();
        inner?.Invoke(obj);
        return obj;
    }

    IWhereQueryBuilder<T> IWhereQueryBuilder<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value)
    {
        return EqualTo(column, value);
    }

    IWhereQueryBuilder<T> IWhereQueryBuilder<T>.And()
    {
        return And();
    }
}