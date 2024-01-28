using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IQBDelete<T>
    where T : IHasTable
{
    IQBDelete<T> Delete();
    IQBDelete<T> Where(Action<IWhereBuilder<T>> inner);
}

public class QBDelete<T> : QBCore, IQBDelete<T>
    where T : IHasTable
{
    public IQBDelete<T> Delete()
    {
        new TableTranslator("delete", T.GetTable()).Run(context);
        return this;
    }

    public IQBDelete<T> Where(Action<IWhereBuilder<T>> inner)
    {
        new CommandTranslator("where").Run(context);
        Make<WhereBuilder<T>>(context, inner);
        return this;
    }
}
