using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms;

public partial class MsQueryBuilder : Core.QueryBuilder
{
    protected MsQueryBuilder(QueryBuilderSource source) : base(source) { }

    public MsDeleteQueryBuilder<T> Delete<T>() => 
        MsDeleteQueryBuilder<T>.Make(Source);

    public MsDeleteQueryBuilder<T> Delete<T>(Action<MsTableTranslator<T>> inner) => 
        MsDeleteQueryBuilder<T>.Make(Source).Delete(inner);
}