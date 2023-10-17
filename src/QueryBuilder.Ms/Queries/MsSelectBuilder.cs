using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectBuilder<T>
    where T : ITableBuilder
{
    IMsSelectBuilder<T> All();
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

    public static MsSelectBuilder<T> Make(QueryBuilderSource source, Action<MsSelectBuilder<T>> inner)
    {
        var obj = new MsSelectBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }

    IMsSelectBuilder<T> IMsSelectBuilder<T>.All()
        => All();
}


public interface IMsSelectBuilder<TLeft, TRight>
    where TLeft : ITableBuilder
    where TRight : ITableBuilder
{
    IMsSelectBuilder<T> Bind<T>() where T : ITableBuilder;
}

public class MsSelectBuilder<TLeft, TRight> : QueryBuilderCore, IMsSelectBuilder<TLeft, TRight>
    where TLeft : ITableBuilder
    where TRight : ITableBuilder
{
    public MsSelectBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsSelectBuilder<T> Bind<T>()
        where T : ITableBuilder
    {
        return MsSelectBuilder<T>.Make(Source, null);
    }

    public static MsSelectBuilder<TLeft, TRight> Make(QueryBuilderSource source, Action<MsSelectBuilder<TLeft, TRight>> inner)
    {
        var obj = new MsSelectBuilder<TLeft, TRight>(source);
        inner?.Invoke(obj);
        return obj;
    }

    IMsSelectBuilder<T> IMsSelectBuilder<TLeft, TRight>.Bind<T>() 
        => Bind<T>();
}