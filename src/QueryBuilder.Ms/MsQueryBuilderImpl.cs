using QueryBuilder.Core.Queris;
using QueryBuilder.Ms.Queris;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    protected MsQueryBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public partial IMsDeleteQueryBuilder<T> Delete<T>()
    {
        return MsDeleteQueryBuilder<T>.Make(Source);
    }

    public partial IMsDeleteQueryBuilder<T> Delete<T>(Action<IMsTableTranslator<T>> inner)
    {
        return MsDeleteQueryBuilder<T>.Make(Source).Delete(inner);
    }
}