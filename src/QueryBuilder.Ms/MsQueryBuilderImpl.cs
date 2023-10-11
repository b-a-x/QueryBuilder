using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    public MsQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsDeleteQueryBuilder<T> Delete<T>(string schema = null, string table = null, string alias = null) => 
        MsDeleteQueryBuilder<T>.Make(Source).Delete(schema, table, alias);
}