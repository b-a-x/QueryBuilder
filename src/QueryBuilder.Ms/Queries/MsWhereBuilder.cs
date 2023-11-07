using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereBuilder<T>
    where T : ITableBuilder
{
    IMsWhereBuilder<TDto> Bind<TDto>() where TDto : ITableBuilder;
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

public class MsWhereBuilder<T> : QueryBuilderCore, IMsWhereBuilder<T>
    where T : ITableBuilder
{
    public MsWhereBuilder(QueryBuilderSource source) : base(source) { }

    public IMsWhereBuilder<T> Where()
    {
        new CommandTranslator("where").Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        new EqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> And()
    {
        new AndTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> Or()
    {
        new OrTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<TDto> Bind<TDto>()
        where TDto : ITableBuilder 
        => MsWhereBuilder<TDto>.Make(Source, null);

    public IMsWhereBuilder<T> Bracket(Action inner)
    {
        var bracket = new BracketTranslator();
        bracket.BeforeRun(Source);
        inner?.Invoke();
        bracket.AfterRun(Source);
        return this;
    }

    public IMsWhereBuilder<T> MoreEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new MoreEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> LessEqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new LessEqualToTranslator(CommonExpression.GetColumnName(column), value, T.GetTable()).Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> IsNull()
    {
        new IsNullTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> IsNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNullTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> IsNotNull()
    {
        new IsNotNullTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> IsNotNull<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        Column(column);
        new IsNotNullTranslator().Run(Source);
        return this;
    }

    public IMsWhereBuilder<T> Column<TField>([NotNull] Expression<Func<T, TField>> column)
    {
        new ColumnTranslator(CommonExpression.GetColumnName(column), T.GetTable()).Run(Source);
        return this;
    }

    public static MsWhereBuilder<T> Make(QueryBuilderSource source, Action<MsWhereBuilder<T>> inner)
    {
        var obj = new MsWhereBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }

    public static MsWhereBuilder<T> MakeWhere(QueryBuilderSource source, Action<MsWhereBuilder<T>> inner)
    {
        var obj = new MsWhereBuilder<T>(source);
        obj.Where();
        inner?.Invoke(obj);
        return obj;
    }
}