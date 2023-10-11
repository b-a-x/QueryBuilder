using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereQueryBuilder<T> : IWhereQueryBuilder<T>
{
    IMsWhereQueryBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereQueryBuilder<T> And();
}

public class MsWhereQueryBuilder<T> : WhereQueryBuilder<T>, IMsWhereQueryBuilder<T>
{
    public MsWhereQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsWhereQueryBuilder<T> Where()
    {
        CommandTranslator.Make("where").Run(Source);
        return this;
    }

    public MsWhereQueryBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        EqualToTranslator.Make(CommonExpression.GetColumnName(column), value).Run(Source);
        return this;
    }

    public MsWhereQueryBuilder<T> And()
    {
        AndTranslator.Make().Run(Source);
        return this;
    }

    public static MsWhereQueryBuilder<T> Make(QueryBuilderSource source, Action<MsWhereQueryBuilder<T>> inner)
    {
        var obj = new MsWhereQueryBuilder<T>(source).Where();
        inner?.Invoke(obj);
        return obj;
    }

    IMsWhereQueryBuilder<T> IMsWhereQueryBuilder<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value)
    {
        return EqualTo(column, value);
    }

    IMsWhereQueryBuilder<T> IMsWhereQueryBuilder<T>.And()
    {
        return And();
    }
}
