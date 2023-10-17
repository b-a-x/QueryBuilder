using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IMsJoinBuilder<TLeft, TRigth>
    where TRigth : ITableBuilder
{
    IMsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
    IMsJoinBuilder<TLeft, TRigth> And();
}

public class MsJoinBuilder<TLeft, TRigth> : QueryBuilderCore, IMsJoinBuilder<TLeft, TRigth>
    where TRigth : ITableBuilder
{
    protected MsJoinBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsJoinBuilder<TLeft, TRigth> Join()
    {
        JoinTranslator<TRigth>.Make("join").Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        EqualTranslator.Make(CommonExpression.GetColumnName(columnLeft), CommonExpression.GetColumnName(columnRigth)).Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> And()
    {
        AndTranslator.Make().Run(Source);
        return this;
    }

    public static MsJoinBuilder<TLeft, TRigth> Make(QueryBuilderSource source, Action<MsJoinBuilder<TLeft, TRigth>> inner)
    {
        var obj = new MsJoinBuilder<TLeft, TRigth>(source).Join();
        inner?.Invoke(obj);
        return obj;
    }

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.And()
        => And();

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
        => EqualTo(columnLeft, columnRigth);
}