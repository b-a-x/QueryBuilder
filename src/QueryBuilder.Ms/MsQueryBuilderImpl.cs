using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public IMsDeleteQueryBuilder<T> Delete<T>()
        where T : IHasTable
        => QBCore.Make<MsDeleteQueryBuilder<T>>(Context).Delete();

    public IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner)
        where T : IHasTable 
        => QBCore.Make<MsSelectQueryBuilder<T>>(Context).Select(inner);
}