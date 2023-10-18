using QueryBuilder.Core;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder, ISelect
{
    IMsDeleteQueryBuilder<T> Delete<T>() 
        where T : ITableBuilder;
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    IMsSelectQueryBuilder<T> ISelect.Select<T>(Action<IMsSelectBuilder<T>> inner)
        => Select(inner);

    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>()
        => Delete<T>();
}