using QueryBuilder.Core;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder
{
    IMsDeleteQueryBuilder<T> Delete<T>(string schema = null, string table = null, string alias = null);
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>(string schema = null, string table = null, string alias = null) =>
        Delete<T>(schema, table, alias);
}