using QueryBuilder.Ms.Queris;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms;

//TODO: Как использовать общий билдер и нужно ли его использовать
public interface IMsQueryBuilder //: IQueryBuilder
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