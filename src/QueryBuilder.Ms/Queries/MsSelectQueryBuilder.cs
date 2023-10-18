using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
    where T : ITableBuilder
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
    IMsSelectQueryBuilder<T> Join<TRigth>(Action<IMsJoinBuilder<T, TRigth>> inner)
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<T> Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : ITableBuilder
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<T> Where(Action<IMsWhereBuilder<T>> inner);
}

public class MsSelectQueryBuilder<T> : QueryBuilderCore, IMsSelectQueryBuilder<T>
    where T : ITableBuilder
{
    public MsSelectQueryBuilder(QueryBuilderSource source) : base(source) {}

    public MsSelectQueryBuilder<T> Select()
    {
        CommandTranslator.Make("select").Run(Source);
        return this;
    }

    public MsSelectQueryBuilder<T> Select(Action<MsSelectBuilder<T>> inner)
    {
        Select();
        MsSelectBuilder<T>.Make(Source, inner);
        From();
        return this;
    }

    public MsSelectQueryBuilder<T> From()
    {
        TableTranslator<T>.Make("from").Run(Source);
        return this;
    }

    public MsSelectQueryBuilder<T> Where(Action<MsWhereBuilder<T>> inner)
    {
        MsWhereBuilder<T>.MakeWhere(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<T> Join<TRigth>(Action<MsJoinBuilder<T, TRigth>> inner)
        where TRigth : ITableBuilder
    {
        MsJoinBuilder<T, TRigth>.Make(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<T> Join<TLeft, TRigth>(Action<MsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : ITableBuilder
        where TRigth : ITableBuilder
    {
        MsJoinBuilder<TLeft, TRigth>.Make(Source, inner);
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

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner) 
        => Join(inner);

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Join<TRigth>(Action<IMsJoinBuilder<T, TRigth>> inner) 
        => Join<TRigth>(inner);
}