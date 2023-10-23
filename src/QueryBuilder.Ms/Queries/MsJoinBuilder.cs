using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IMsJoinBuilder<TLeft, TRigth>
    where TLeft : ITableBuilder
    where TRigth : ITableBuilder
{
    IMsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
    IMsJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth);
    IMsJoinBuilder<TLeft, TRigth> And();
    IMsJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
}

public class MsJoinBuilder<TLeft, TRigth> : QueryBuilderCore, IMsJoinBuilder<TLeft, TRigth>
    where TLeft : ITableBuilder
    where TRigth : ITableBuilder
{
    protected MsJoinBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsJoinBuilder<TLeft, TRigth> Join()
    {
        new JoinTranslator("join", TRigth.GetTable()).Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> LeftJoin()
    {
        new JoinTranslator("left join", TRigth.GetTable()).Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        new EqualTranslator(CommonExpression.GetColumnName(columnLeft), CommonExpression.GetColumnName(columnRigth), TLeft.GetTable(), TRigth.GetTable()).Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth)
    {
        new EqualTranslator(columnLeft, columnRigth, TLeft.GetTable(), TRigth.GetTable()).Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> And()
    {
        new AndTranslator().Run(Source);
        return this;
    }

    public MsJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        throw new NotImplementedException();
    }

    public static MsJoinBuilder<TLeft, TRigth> JoinMake(QueryBuilderSource source, Action<MsJoinBuilder<TLeft, TRigth>> inner)
    {
        var obj = new MsJoinBuilder<TLeft, TRigth>(source).Join();
        inner?.Invoke(obj);
        return obj;
    }

    public static MsJoinBuilder<TLeft, TRigth> LeftJoinMake(QueryBuilderSource source, Action<MsJoinBuilder<TLeft, TRigth>> inner)
    {
        var obj = new MsJoinBuilder<TLeft, TRigth>(source).LeftJoin();
        inner?.Invoke(obj);
        return obj;
    }

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.And()
        => And();

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
        => EqualTo(columnLeft, columnRigth);

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.EqualTo(string columnLeft, string columnRigth) 
        => EqualTo(columnLeft, columnRigth);

    IMsJoinBuilder<TLeft, TRigth> IMsJoinBuilder<TLeft, TRigth>.Between<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        throw new NotImplementedException();
    }
}