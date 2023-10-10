using QueryBuilder.Core.Queris;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core;

public partial class QueryBuilder : QueryBuilderCore
{
    public QueryBuilder(QueryBuilderSource source) : base(source) { }

    public partial IDeleteQueryBuilder<T> Delete<T>() 
    { 
        return DeleteQueryBuilder<T>.Make(Source).Delete(); 
    }

    public partial IDeleteQueryBuilder<T> Delete<T>(Action<ITableTranslator<T>> inner)
    {
        return DeleteQueryBuilder<T>.Make(Source).Delete(inner);
    }
}
