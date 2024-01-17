using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public MsQueryBuilder(QueryBuilderContext source) : base(source) { }

    public MsDeleteQueryBuilder<T> Delete<T>()
        where T : IHasTable
        => MsDeleteQueryBuilder<T>.Make(Context).Delete();

    public MsSelectQueryBuilder<T> Select<T>(Action<MsSelectBuilder<T>> inner)
        where T : IHasTable 
        => MsSelectQueryBuilder<T>.Make(Context, null).Select(inner);
}