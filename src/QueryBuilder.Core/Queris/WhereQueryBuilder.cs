namespace QueryBuilder.Core.Queris;

public interface IWhereQueryBuilder<T> { }

public class WhereQueryBuilder<T> : QueryBuilderCore, IWhereQueryBuilder<T>
{
    public WhereQueryBuilder(QueryBuilderSource source) : base(source) { }
}