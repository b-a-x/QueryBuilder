using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereBuilder<T> : IWhereBuilder<T>
    where T : ITableBuilder
{
    IMsSelectBuilder<TDto> Bind<TDto>() where TDto : ITableBuilder;
    IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> And();
}

public class MsWhereBuilder<T> : QueryBuilderCore, IMsWhereBuilder<T>
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

    public MsWhereBuilder<TDto> Bind<TDto>()
        where TDto : ITableBuilder 
        => MsWhereBuilder<TDto>.Make(Source, null);

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

    IMsWhereBuilder<T> IMsWhereBuilder<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value) =>
       EqualTo(column, value);

    IMsWhereBuilder<T> IMsWhereBuilder<T>.And() =>
       And();

    IMsSelectBuilder<TDto> IMsWhereBuilder<T>.Bind<TDto>() 
        => throw new NotImplementedException();
}