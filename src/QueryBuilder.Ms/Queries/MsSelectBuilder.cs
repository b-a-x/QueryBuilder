using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectBuilder<T>
    where T : ITableBuilder
{
    IMsSelectBuilder<TDto> Bind<TDto>() where TDto : ITableBuilder;
    IMsSelectBuilder<T> All();
    IMsSelectBuilder<T> Field<TField>([NotNull] Expression<Func<T, TField>> column);
    IMsSelectBuilder<T> Field(string column);
    IMsSelectBuilder<T> As(string value);
}

public class MsSelectBuilder<T> : QueryBuilderCore, IMsSelectBuilder<T>
    where T : ITableBuilder
{
    public MsSelectBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsSelectBuilder<T> All()
    {
        AllTranslator<T>.Make().Run(Source);
        return this;
    }

    public MsSelectBuilder<T> As(string value)
    {
        AsTranslator.Make(value).Run(Source);
        return this;
    }

    public MsSelectBuilder<T> Field<TField>(Expression<Func<T, TField>> column)
    {
        FieldTranslator<T>.Make(CommonExpression.GetColumnName(column)).Run(Source);
        return this;
    }

    public MsSelectBuilder<T> Field(string column)
    {
        FieldTranslator<T>.Make(column).Run(Source);
        return this;
    }

    public MsSelectBuilder<TDto> Bind<TDto>() 
        where TDto : ITableBuilder
    {
        return MsSelectBuilder<TDto>.Make(Source, null);
    }

    public static MsSelectBuilder<T> Make(QueryBuilderSource source, Action<MsSelectBuilder<T>> inner)
    {
        var obj = new MsSelectBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }

    IMsSelectBuilder<T> IMsSelectBuilder<T>.All()
        => All();

    IMsSelectBuilder<T> IMsSelectBuilder<T>.Field<TField>(Expression<Func<T, TField>> column) 
        => Field(column);

    IMsSelectBuilder<T> IMsSelectBuilder<T>.As(string value) 
        => As(value);

    IMsSelectBuilder<TDto> IMsSelectBuilder<T>.Bind<TDto>() 
        => Bind<TDto>();

    IMsSelectBuilder<T> IMsSelectBuilder<T>.Field(string column) 
        => Field(column);
}