namespace QueryBuilder.Core.Queris;

public interface IDeleteQueryBuilder<T> { }

public class DeleteQueryBuilder<T> : QueryBuilderCore, IDeleteQueryBuilder<T>
{
    public DeleteQueryBuilder(QueryBuilderSource source) : base(source) {}
}
