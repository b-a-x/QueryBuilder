using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereBuilder<T> : IWhereBuilder<T>
    where T : ITableBuilder
{
    IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> And();
}

public class MsWhereBuilder<T> : WhereQueryBuilder<T>, IMsWhereBuilder<T>
    where T : ITableBuilder
{
    public MsWhereBuilder(QueryBuilderSource source) : base(source) { }

    public MsWhereBuilder<T> Where()
    {
        CommandTranslator.Make("where").Run(Source);
        return this;
    }

    public MsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        EqualToTranslator<T>.Make(CommonExpression.GetColumnName(column), value).Run(Source);
        return this;
    }

    public MsWhereBuilder<T> And()
    {
        AndTranslator.Make().Run(Source);
        return this;
    }

    public static MsWhereBuilder<T> Make(QueryBuilderSource source, Action<MsWhereBuilder<T>> inner)
    {
        var obj = new MsWhereBuilder<T>(source).Where();
        inner?.Invoke(obj);
        return obj;
    }

    IMsWhereBuilder<T> IMsWhereBuilder<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value) =>
       EqualTo(column, value);

    IMsWhereBuilder<T> IMsWhereBuilder<T>.And() =>
       And();
}
