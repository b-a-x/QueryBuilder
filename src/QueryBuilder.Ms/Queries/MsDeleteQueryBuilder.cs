using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsDeleteQueryBuilder<T>
{
    IMsDeleteQueryBuilder<T> Delete(string schema = null, string table = null, string alias = null);
    IMsDeleteQueryBuilder<T> Where(Action<IMsWhereQueryBuilder<T>> inner);
}

public class MsDeleteQueryBuilder<T> : QueryBuilderCore, IMsDeleteQueryBuilder<T>
{
    public MsDeleteQueryBuilder(QueryBuilderSource source) : base(source) { }

    public static MsDeleteQueryBuilder<T> Make(QueryBuilderSource source) => 
        new MsDeleteQueryBuilder<T>(source);

    public MsDeleteQueryBuilder<T> Delete(string schema = null, string table = null, string alias = null)
    {
        TableTranslator<T>.Make("delete", "dbo", table, alias).Run(Source);
        return this;
    }

    public MsDeleteQueryBuilder<T> Where(Action<IMsWhereQueryBuilder<T>> inner)
    {
        MsWhereQueryBuilder<T>.Make(Source, inner);
        return this;
    }

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Delete(string schema = null, string table = null, string alias = null) => 
        Delete(schema, table, alias);

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Where(Action<IMsWhereQueryBuilder<T>> inner) => 
        Where(inner);
}
