using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Queries;

public interface IDeleteQueryBuilder<T> { }

public class DeleteQueryBuilder<T> : QBCore, IDeleteQueryBuilder<T>
{
}
