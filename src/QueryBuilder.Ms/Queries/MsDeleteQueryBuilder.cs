using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsDeleteQueryBuilder<T>
    where T : IHasTable
{
    IMsDeleteQueryBuilder<T> Delete();
    IMsDeleteQueryBuilder<T> Where(Action<IMsWhereBuilder<T>> inner);
}

public class MsDeleteQueryBuilder<T> : QueryBuilderCore, IMsDeleteQueryBuilder<T>
    where T : IHasTable
{
    public MsDeleteQueryBuilder(QueryBuilderContext source) : base(source) { }

    public static MsDeleteQueryBuilder<T> Make(QueryBuilderContext source) => 
        new MsDeleteQueryBuilder<T>(source);

    public MsDeleteQueryBuilder<T> Delete()
    {
        new TableTranslator("delete", T.GetTable()).Run(Context);
        return this;
    }

    public MsDeleteQueryBuilder<T> Where(Action<MsWhereBuilder<T>> inner)
    {
        MsWhereBuilder<T>.MakeWhere(Context, inner);
        return this;
    }

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Delete() => 
        Delete();

    IMsDeleteQueryBuilder<T> IMsDeleteQueryBuilder<T>.Where(Action<IMsWhereBuilder<T>> inner) => 
        Where(inner);
}
