using QueryBuilder.Core;
using QueryBuilder.Core.Helpers;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder
{
    IMsDeleteQueryBuilder<T> Delete<T>() 
        where T : ITableBuilder;
    IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner) 
        where T : ITableBuilder;
    IMsSelectQueryBuilder<TOne, TTwo> Select<TOne, TTwo>(Action<IMsSelectBuilder<TOne, TTwo>> inner) 
        where TOne : ITableBuilder
        where TTwo : ITableBuilder;
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    IMsSelectQueryBuilder<T> IMsQueryBuilder.Select<T>(Action<IMsSelectBuilder<T>> inner)
        => Select(inner);

    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>()
        => Delete<T>();

    IMsSelectQueryBuilder<TOne, TTwo> IMsQueryBuilder.Select<TOne, TTwo>(Action<IMsSelectBuilder<TOne, TTwo>> inner) 
        => Select(inner);
}