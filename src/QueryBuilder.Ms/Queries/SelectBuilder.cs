using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Syntax;
using QueryBuilder.Ms.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface ISelectBuilder<T> : IsNullBuilder<T, ISelectBuilder<T>>
    where T : IHasTable
{
    ISelectBuilder<TDto> Bind<TDto>() where TDto : IHasTable;
    ISelectBuilder<T> All();
    ISelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column, bool isComma = true);
    ISelectBuilder<T> Column(string column, bool isComma = true);
    ISelectBuilder<T> As(string value);
    ISelectBuilder<T> IsNullFunc<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    ISelectBuilder<T> Case(Action<ICaseBuilder<T>> builder);
}

public class SelectBuilder<T> : QBCore, ISelectBuilder<T>
    where T : IHasTable
{
    public ISelectBuilder<T> All()
    {
        new CommaSelectTranslator().Run(context);
        new AllTranslator(T.GetTable()).Run(context);
        return this;
    }

    public ISelectBuilder<T> As(string value)
    {
        new AsTranslator(value).Run(context);
        return this;
    }

    public ISelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column, bool isComma = true)
    {
        Column(CommonExpression.GetColumnName(column), isComma);
        return this;
    }

    public ISelectBuilder<T> Column(string column, bool isComma = true)
    {
        if(isComma) new CommaSelectTranslator().Run(context);
        new ColumnTranslator(column, T.GetTable()).Run(context);
        return this;
    }

    public ISelectBuilder<TDto> Bind<TDto>() where TDto : IHasTable => 
        QBCore.Make<SelectBuilder<TDto>>(context);

    public ISelectBuilder<T> IsNullFunc<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new CommaSelectTranslator().Run(context);
        new IsNullFuncTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(context);
        return this;
    }

    public ISelectBuilder<T> Case(Action<ICaseBuilder<T>> builder)
    {
        new CommaSelectTranslator().Run(context);
        Make<CaseBuilder<T>>(context).Case(builder);
        return this;
    }

    public ISelectBuilder<T> IsNull()
    {
        new IsNullTranslator().Run(context);
        return this;
    }

    public ISelectBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column, false);
        IsNull();
        return this;
    }

    public ISelectBuilder<T> IsNull([NotNull] string column)
    {
        Column(column, false);
        IsNull();
        return this;
    }
}