using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Entity;
using QueryBuilder.Ms.Translators;
using QueryBuilder.Core.Context;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IWhereBuilder<T>
    where T : IHasTable
{
    IWhereBuilder<TDto> Bind<TDto>() where TDto : IHasTable;
    IWhereBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column);
    IWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IWhereBuilder<T> MoreEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IWhereBuilder<T> LessEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IWhereBuilder<T> And();
    IWhereBuilder<T> Or();

    /// <summary>
    /// Синтаксис ( ... )
    /// </summary>
    /// <param name="inner"></param>
    /// <returns></returns>
    IWhereBuilder<T> Bracket(Action inner);
    IWhereBuilder<T> IsNull();
    IWhereBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column);
    IWhereBuilder<T> IsNotNull();
    IWhereBuilder<T> IsNotNull<TField>([NotNull] Expression<Func<T, TField>> column);
}

public class WhereBuilder<T> : QBCore, IWhereBuilder<T>
    where T : IHasTable
{
    public IWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        new EqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(context);
        return this;
    }

    public IWhereBuilder<T> And()
    {
        new AndTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<T> Or()
    {
        new OrTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<TDto> Bind<TDto>()
        where TDto : IHasTable 
        => QBCore.Make<WhereBuilder<TDto>>(context);

    public IWhereBuilder<T> Bracket(Action inner)
    {
        var bracket = new BracketTranslator();
        bracket.BeforeRun(context);
        inner?.Invoke();
        bracket.AfterRun(context);
        return this;
    }

    public IWhereBuilder<T> MoreEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new MoreEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(context);
        return this;
    }

    public IWhereBuilder<T> LessEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new LessEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(context);
        return this;
    }

    public IWhereBuilder<T> IsNull()
    {
        new IsNullTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNullTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<T> IsNotNull()
    {
        new IsNotNullTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<T> IsNotNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNotNullTranslator().Run(context);
        return this;
    }

    public IWhereBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        new ColumnTranslator(CommonExpression.GetColumnName(column), T.GetTable()).Run(context);
        return this;
    }
}