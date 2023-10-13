using QueryBuilder.Core;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder
{
    IMsDeleteQueryBuilder<T> Delete<T>() where T : ITableBuilder;
    IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner);
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    public IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner)
    {
        throw new NotImplementedException();
    }

    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>()
        => Delete<T>();
}