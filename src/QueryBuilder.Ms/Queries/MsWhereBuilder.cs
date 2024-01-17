﻿using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereBuilder<T>
    where T : IHasTable
{
    IMsWhereBuilder<TDto> Bind<TDto>() where TDto : IHasTable;
    IMsWhereBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column);
    IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> MoreEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> LessEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> And();
    IMsWhereBuilder<T> Or();
    IMsWhereBuilder<T> Bracket(Action inner);
    IMsWhereBuilder<T> IsNull();
    IMsWhereBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column);
    IMsWhereBuilder<T> IsNotNull();
    IMsWhereBuilder<T> IsNotNull<TField>([NotNull] Expression<Func<T, TField>> column);
}

public class MsWhereBuilder<T> : QBCore, IMsWhereBuilder<T>
    where T : IHasTable
{
    public IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        new EqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> And()
    {
        new AndTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> Or()
    {
        new OrTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<TDto> Bind<TDto>()
        where TDto : IHasTable 
        => QBCore.Make<MsWhereBuilder<TDto>>(Context);

    public IMsWhereBuilder<T> Bracket(Action inner)
    {
        var bracket = new BracketTranslator();
        bracket.BeforeRun(Context);
        inner?.Invoke();
        bracket.AfterRun(Context);
        return this;
    }

    public IMsWhereBuilder<T> MoreEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new MoreEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> LessEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new LessEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> IsNull()
    {
        new IsNullTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNullTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> IsNotNull()
    {
        new IsNotNullTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> IsNotNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNotNullTranslator().Run(Context);
        return this;
    }

    public IMsWhereBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        new ColumnTranslator(CommonExpression.GetColumnName(column), T.GetTable()).Run(Context);
        return this;
    }
}