using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsDeleteQueryBuilder<T>
{
    IMsDeleteQueryBuilder<T> Delete();
    IMsDeleteQueryBuilder<T> Delete(Action<IMsTableTranslator<T>> inner);
}

public class MsDeleteQueryBuilder<T> : QueryBuilderCore, IMsDeleteQueryBuilder<T>
{
    public MsDeleteQueryBuilder(QueryBuilderSource source) : base(source) { }

    public static MsDeleteQueryBuilder<T> Make(QueryBuilderSource source)
    {
        return new MsDeleteQueryBuilder<T>(source);
    }

    public MsDeleteQueryBuilder<T> Delete(Action<MsTableTranslator<T>> inner)
    {
        MsTableTranslator<T>.Make("delete", inner).Run(Source);
        return this;
    }

    public MsDeleteQueryBuilder<T> Delete()
    {
        return Delete(null);
    }

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Delete(Action<IMsTableTranslator<T>> inner)
    {
        return Delete(inner);
    }

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Delete()
    {
        return Delete(null);
    }
}
