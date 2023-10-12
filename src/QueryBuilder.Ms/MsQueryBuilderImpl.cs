using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Helpers;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public MsQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsDeleteQueryBuilder<T> Delete<T>()
        where T : IMsTableTranslator
        => MsDeleteQueryBuilder<T>.Make(Source).Delete();
}