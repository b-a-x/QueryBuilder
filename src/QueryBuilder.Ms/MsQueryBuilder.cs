using QueryBuilder.Core;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder
{
    IMsDeleteQueryBuilder<T> Delete<T>(string schema = null, string table = null, string alias = null);
    IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner);
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    public IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner)
    {
        throw new NotImplementedException();
    }

    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>(string schema = null, string table = null, string alias = null) =>
        Delete<T>(schema, table, alias);
}