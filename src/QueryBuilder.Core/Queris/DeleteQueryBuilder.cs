using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core.Queris;

public interface IDeleteQueryBuilder<T>
{
    IDeleteQueryBuilder<T> Delete();
    IDeleteQueryBuilder<T> Where(Action<IWhereTranslator<T>> where);
}

public class DeleteQueryBuilder<T> : QueryBuilderCore, IDeleteQueryBuilder<T>
{
    public DeleteQueryBuilder(QueryBuilderSource source) : base(source) {}

    public static DeleteQueryBuilder<T> Make(QueryBuilderSource source)
    {
        return new DeleteQueryBuilder<T>(source);
    }

    public DeleteQueryBuilder<T> Delete()
    {
        SmartTableTranslator<T>.Make("delete").Run(Source);
        return this;
    }

    public DeleteQueryBuilder<T> Where(Action<WhereTranslator<T>> inner)
    {
        WhereTranslator<T>.Make(inner).Run(Source);
        return this;
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Delete()
    {
        return Delete();
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Where(Action<IWhereTranslator<T>> where)
    {
        return Where(where);
    }
}
