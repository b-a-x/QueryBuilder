using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public MsQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsSelectQueryBuilder<T> Select<T>(Action<MsSelectBuilder<T>> inner)
        where T : ITableBuilder
    {
        var builder = MsSelectQueryBuilder<T>.Make(Source).Select();
        MsSelectBuilder<T>.Make(Source, inner);
        return builder.From();
    }

    public MsDeleteQueryBuilder<T> Delete<T>()
        where T : ITableBuilder
        => MsDeleteQueryBuilder<T>.Make(Source).Delete();
}