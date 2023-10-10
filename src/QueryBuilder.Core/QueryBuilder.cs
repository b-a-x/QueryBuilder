using QueryBuilder.Core.Queris;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core
{
    public interface IQueryBuilder
    {
        IDeleteQueryBuilder<T> Delete<T>();
        IDeleteQueryBuilder<T> Delete<T>(Action<ITableTranslator<T>> inner);
    }

    public partial class QueryBuilder : IQueryBuilder
    {
        public partial IDeleteQueryBuilder<T> Delete<T>();

        public partial IDeleteQueryBuilder<T> Delete<T>(Action<ITableTranslator<T>> inner);
    }
}