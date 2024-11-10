using QueryBuilder.Core.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Syntax;

public interface IsNullBuilder<T, TOut>
    where T : IHasTable
    where TOut : class
{
    /// <summary>
    /// syntax: is null
    /// </summary>
    /// <returns></returns>
    TOut IsNull();

    /// <summary>
    /// syntax: is null
    /// </summary>
    /// <returns></returns>
    TOut IsNull<TField>([NotNull] Expression<Func<T, TField>> column);

    /// <summary>
    /// syntax: is null
    /// </summary>
    /// <returns></returns>
    TOut IsNull([NotNull] string column);
}