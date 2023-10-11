using QueryBuilder.Core;
using QueryBuilder.Ms.Queries;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms;

public interface IMsQueryBuilder : IQueryBuilder
{
    IMsDeleteQueryBuilder<T> Delete<T>();
    IMsDeleteQueryBuilder<T> Delete<T>(Action<IMsTableTranslator<T>> inner);
}

public partial class MsQueryBuilder : IMsQueryBuilder
{
    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>() =>
        Delete<T>();

    IMsDeleteQueryBuilder<T> IMsQueryBuilder.Delete<T>(Action<IMsTableTranslator<T>> inner) =>
        Delete<T>(inner);
}