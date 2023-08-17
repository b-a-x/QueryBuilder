using QueryBuilder.Core.Queris;

namespace QueryBuilder.Core
{
    public interface IQueryBuilder
    {
        IDeleteQueryBuilder<T> Delete<T>();
    }

    public partial class QueryBuilder : IQueryBuilder
    {
        public partial IDeleteQueryBuilder<T> Delete<T>();
    }
}