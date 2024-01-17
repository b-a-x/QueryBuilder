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

public class MsDeleteQueryBuilder<T> : QBCore, IMsDeleteQueryBuilder<T>
    where T : IHasTable
{
    public IMsDeleteQueryBuilder<T> Delete()
    {
        new TableTranslator("delete", T.GetTable()).Run(Context);
        return this;
    }

    public IMsDeleteQueryBuilder<T> Where(Action<IMsWhereBuilder<T>> inner)
    {
        new CommandTranslator("where").Run(Context);
        QBCore.Make<MsWhereBuilder<T>>(Context, inner);
        return this;
    }
}
