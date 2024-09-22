using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface ISelectBuilder<T>
    where T : IHasTable
{
    ISelectBuilder<TDto> Bind<TDto>() where TDto : IHasTable;
    ISelectBuilder<T> All();
    ISelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column);
    ISelectBuilder<T> Column(string column);
    ISelectBuilder<T> As(string value);
    ISelectBuilder<T> IsNullFunc<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    ISelectBuilder<T> Case(Action<ICaseBuilder<T>> inner);
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

    public ISelectBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(CommonExpression.GetColumnName(column));
        return this;
    }

    public ISelectBuilder<T> Column(string column)
    {
        new CommaSelectTranslator().Run(context);
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

    public ISelectBuilder<T> Case(Action<ICaseBuilder<T>> inner)
    {
        QBCore.Make<CaseBuilder<T>>(context: context, inner: inner);
        return this;
    }
}