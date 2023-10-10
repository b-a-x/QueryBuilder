using QueryBuilder.Core.Queris;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core;

public partial class QueryBuilder : QueryBuilderCore
{
    public QueryBuilder(QueryBuilderSource source) : base(source) { }

    public DeleteQueryBuilder<T> Delete<T>() => 
        DeleteQueryBuilder<T>.Make(Source).Delete();

    public DeleteQueryBuilder<T> Delete<T>(Action<TableTranslator<T>> inner) =>
        DeleteQueryBuilder<T>.Make(Source).Delete(inner);
}
