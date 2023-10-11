using QueryBuilder.Core.Queries;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
}

public class MsSelectQueryBuilder<T> : QueryBuilderCore, IMsSelectQueryBuilder<T>
{
    protected MsSelectQueryBuilder(QueryBuilderSource source) : base(source) {}

    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Select(Action<IMsSelectBuilder<T>> inner)
    {
        throw new NotImplementedException();
    }

    public static MsSelectQueryBuilder<T> Make(QueryBuilderSource source, Action<MsSelectQueryBuilder<T>> inner)
    {
        var obj = new MsSelectQueryBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }
}
