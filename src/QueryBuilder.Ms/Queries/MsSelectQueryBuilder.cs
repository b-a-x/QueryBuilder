using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
    where T : ITableBuilder
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
    //IMsSleectQueryBuilder<T> Join(Action<IMsJoinBuilder<T, T>> inner);
    IMsSelectQueryBuilder<T> Where(Action<IMsWhereBuilder<T>> inner);
}

public class SelectQueryBuilder<T> : QueryBuilderCore
    where T : ITableBuilder
{
    protected SelectQueryBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public void Select()
        => CommandTranslator.Make("select").Run(Source);

    public void From() 
        => TableTranslator<T>.Make("from").Run(Source);
}

public class MsSelectQueryBuilder<T> : SelectQueryBuilder<T>, IMsSelectQueryBuilder<T>
    where T : ITableBuilder
{
    public MsSelectQueryBuilder(QueryBuilderSource source) : base(source) {}

    public MsSelectQueryBuilder<T> Select(Action<MsSelectBuilder<T>> inner)
    {
        Select();
        MsSelectBuilder<T>.Make(Source, inner);
        From();
        return this;
    }

    public MsSelectQueryBuilder<T> Where(Action<MsWhereBuilder<T>> inner)
    {
        MsWhereBuilder<T>.Make(Source, inner);
        return this;
    }

    public static MsSelectQueryBuilder<T> Make(QueryBuilderSource source)
    {
        return new MsSelectQueryBuilder<T>(source);
    }

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Select(Action<IMsSelectBuilder<T>> inner)
        => Select(inner);

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Where(Action<IMsWhereBuilder<T>> inner)
        => Where(inner);
}

public interface IMsSelectQueryBuilder<TOne, TTwo>
    where TOne : ITableBuilder
    where TTwo : ITableBuilder
{
    IMsSelectQueryBuilder<TOne, TTwo> Select(Action<IMsSelectBuilder<TOne>> one, Action<IMsSelectBuilder<TTwo>> two);
    IMsSelectQueryBuilder<TOne, TTwo> Join(Action<IMsJoinBuilder<TOne, TTwo>> inner);
    //IMsSelectQueryBuilder<TOne, TTwo> Where(Action<IMsWhereBuilder<T>> inner);
}

public class MsSelectQueryBuilder<TOne, TTwo> : SelectQueryBuilder<TOne>, IMsSelectQueryBuilder<TOne, TTwo>
    where TOne : ITableBuilder
    where TTwo : ITableBuilder
{
    public MsSelectQueryBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsSelectQueryBuilder<TOne, TTwo> Select(Action<MsSelectBuilder<TOne>> one, Action<MsSelectBuilder<TTwo>> two)
    {
        Select();
        MsSelectBuilder<TOne>.Make(Source, one);
        MsSelectBuilder<TTwo>.Make(Source, two);
        From();
        return this;
    }

    public MsSelectQueryBuilder<TOne, TTwo> Join(Action<MsJoinBuilder<TOne, TTwo>> inner)
    {
        MsJoinBuilder<TOne, TTwo>.Make(Source, inner);
        return this;
    }

    IMsSelectQueryBuilder<TOne, TTwo> IMsSelectQueryBuilder<TOne, TTwo>.Join(Action<IMsJoinBuilder<TOne, TTwo>> inner) 
        => Join(inner);

    IMsSelectQueryBuilder<TOne, TTwo> IMsSelectQueryBuilder<TOne, TTwo>.Select(Action<IMsSelectBuilder<TOne>> one, Action<IMsSelectBuilder<TTwo>> two) 
        => Select(one, two);

    public static MsSelectQueryBuilder<TOne, TTwo> Make(QueryBuilderSource source)
    {
        return new MsSelectQueryBuilder<TOne, TTwo>(source);
    }
}