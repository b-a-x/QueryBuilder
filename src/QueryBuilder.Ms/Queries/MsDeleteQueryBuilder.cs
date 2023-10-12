using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IMsDeleteQueryBuilder<T>
    where T : IMsTableTranslator
{
    IMsDeleteQueryBuilder<T> Delete();
    IMsDeleteQueryBuilder<T> Where(Action<IMsWhereQueryBuilder<T>> inner);
}

public class MsDeleteQueryBuilder<T> : QueryBuilderCore, IMsDeleteQueryBuilder<T>
    where T : IMsTableTranslator
{
    public MsDeleteQueryBuilder(QueryBuilderSource source) : base(source) { }

    public static MsDeleteQueryBuilder<T> Make(QueryBuilderSource source) => 
        new MsDeleteQueryBuilder<T>(source);

    public MsDeleteQueryBuilder<T> Delete()
    {
        MsTableTranslator<T>.Make("delete").Run(Source);
        return this;
    }

    public MsDeleteQueryBuilder<T> Where(Action<IMsWhereQueryBuilder<T>> inner)
    {
        MsWhereQueryBuilder<T>.Make(Source, inner);
        return this;
    }

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Delete() => 
        Delete();

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Where(Action<IMsWhereQueryBuilder<T>> inner) => 
        Where(inner);
}
