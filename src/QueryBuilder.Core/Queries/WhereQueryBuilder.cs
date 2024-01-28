using QueryBuilder.Core.Context;

namespace QueryBuilder.Core.Queries;

public interface IWhereBuilder<T> { }

public class WhereQueryBuilder<T> : QBCore, IWhereBuilder<T>
{
}