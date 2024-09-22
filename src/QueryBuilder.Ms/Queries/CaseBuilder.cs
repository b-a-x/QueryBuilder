using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Ms.Queries;

public interface ICaseBuilder<T>
    where T : IHasTable
{

}

public class CaseBuilder<T> : QBCore, ICaseBuilder<T>
    where T : IHasTable
{
}
