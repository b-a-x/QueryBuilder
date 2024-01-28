using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IJoinBuilder<TLeft, TRigth>
    where TLeft : IHasTable
    where TRigth : IHasTable
{
    IJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
    IJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth);
    IJoinBuilder<TLeft, TRigth> And();
    IJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>([NotNull] Expression<Func<TLeft, TLeftField>> columnLeft, [NotNull] Expression<Func<TRigth, TRigthField>> columnRigth);
}

public class JoinBuilder<TLeft, TRigth> : QBCore, IJoinBuilder<TLeft, TRigth>
    where TLeft : IHasTable
    where TRigth : IHasTable
{
    public IJoinBuilder<TLeft, TRigth> EqualTo<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        new EqualTranslator(CommonExpression.GetColumnName(columnLeft), CommonExpression.GetColumnName(columnRigth), TLeft.GetTable(), TRigth.GetTable()).Run(context);
        return this;
    }

    public IJoinBuilder<TLeft, TRigth> EqualTo(string columnLeft, string columnRigth)
    {
        new EqualTranslator(columnLeft, columnRigth, TLeft.GetTable(), TRigth.GetTable()).Run(context);
        return this;
    }

    public IJoinBuilder<TLeft, TRigth> And()
    {
        new AndTranslator().Run(context);
        return this;
    }

    public IJoinBuilder<TLeft, TRigth> Between<TLeftField, TRigthField>(Expression<Func<TLeft, TLeftField>> columnLeft, Expression<Func<TRigth, TRigthField>> columnRigth)
    {
        throw new NotImplementedException();
    }
}