using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IMsJoinBuilder<TLeft, TRigth>
    where TLeft : IHasTable
    where TRigth : IHasTable
{
    IMsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
    IMsJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth);
    IMsJoinBuilder<TLeft, TRigth> And();
    IMsJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
}

public class MsJoinBuilder<TLeft, TRigth> : QBCore, IMsJoinBuilder<TLeft, TRigth>
    where TLeft : IHasTable
    where TRigth : IHasTable
{
    public IMsJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        new EqualTranslator(CommonExpression.GetColumnName(columnLeft), CommonExpression.GetColumnName(columnRigth), TLeft.GetTable(), TRigth.GetTable()).Run(Context);
        return this;
    }

    public IMsJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth)
    {
        new EqualTranslator(columnLeft, columnRigth, TLeft.GetTable(), TRigth.GetTable()).Run(Context);
        return this;
    }

    public IMsJoinBuilder<TLeft, TRigth> And()
    {
        new AndTranslator().Run(Context);
        return this;
    }

    public IMsJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        throw new NotImplementedException();
    }
}