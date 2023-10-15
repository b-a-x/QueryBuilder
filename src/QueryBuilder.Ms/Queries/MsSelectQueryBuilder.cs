using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
    where T : ITableBuilder
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
    IMsSelectQueryBuilder<T> Where(Action<IWhereBuilder<T>> inner);
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
        return From();
    }

    public MsSelectQueryBuilder<T> Where(Action<MsWhereBuilder<T>> inner)
    {
        MsWhereBuilder<T>.Make(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<T> From()
    {
        TableTranslator<T>.Make("from").Run(Source);
        return this;
    }

    public static MsSelectQueryBuilder<T> Make(QueryBuilderSource source)
    {
        return new MsSelectQueryBuilder<T>(source);
    }

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Select(Action<IMsSelectBuilder<T>> inner)
        => Select(inner);

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Where(Action<IWhereBuilder<T>> inner)
        => Where(inner);
}