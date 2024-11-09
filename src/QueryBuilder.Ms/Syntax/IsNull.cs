using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace QueryBuilder.Ms.Syntax
{
    public interface IsNull
    {
        protected internal void IsNull([NotNull] QBContext context) =>
            new IsNullTranslator().Run(context);
    }

    public interface IsNullBuilder<T, TOut> : IsNull
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
}