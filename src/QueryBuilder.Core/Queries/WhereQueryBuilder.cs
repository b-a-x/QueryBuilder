﻿namespace QueryBuilder.Core.Queries;

public interface IWhereBuilder<T> { }

public class WhereQueryBuilder<T> : QueryBuilderCore, IWhereBuilder<T>
{
    public WhereQueryBuilder(QueryBuilderContext source) : base(source) { }
}