using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms;

public interface IQueryBuilder : ISelect
{
    IQBDelete<T> Delete<T>() 
        where T : IHasTable;
}

public partial class QueryBuilder : QBCore, IQueryBuilder
{
    public IQBDelete<T> Delete<T>()
        where T : IHasTable
        => Make<QBDelete<T>>(context).Delete();

    public IQBSelect<T> Select<T>(Action<ISelectBuilder<T>> inner)
        where T : IHasTable
        => Make<QBSelect<T>>(context).Select(inner);
}