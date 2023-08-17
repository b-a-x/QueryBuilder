using QueryBuilder.Core.Queris;

namespace QueryBuilder.Core;

public partial class QueryBuilder : QueryBuilderCore
{
    public QueryBuilder(QueryBuilderSource source) : base(source) { }

    public partial IDeleteQueryBuilder<T> Delete<T>() 
    { 
        return DeleteQueryBuilder<T>.Make(Source).Delete(); 
    }
}
