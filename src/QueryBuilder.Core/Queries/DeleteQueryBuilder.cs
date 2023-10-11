namespace QueryBuilder.Core.Queries;

public interface IDeleteQueryBuilder<T> { }

public class DeleteQueryBuilder<T> : QueryBuilderCore, IDeleteQueryBuilder<T>
{
    public DeleteQueryBuilder(QueryBuilderSource source) : base(source) {}
}
