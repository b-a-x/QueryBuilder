using QueryBuilder.Core;
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
    public partial IMsDeleteQueryBuilder<T> Delete<T>();
    public partial IMsDeleteQueryBuilder<T> Delete<T>(Action<IMsTableTranslator<T>> inner);
}