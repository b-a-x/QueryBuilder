using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core.Queris;

public interface IDeleteQueryBuilder<T>
{
    IDeleteQueryBuilder<T> Delete();
    IDeleteQueryBuilder<T> Where(Action<IWhereQueryBuilder<T>> where);
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

    public DeleteQueryBuilder<T> Delete(Action<TableTranslator<T>> inner)
    {
        TableTranslator<T>.Make("delete", inner).Run(Source);
        return this;
    }

    public DeleteQueryBuilder<T> Where(Action<WhereQueryBuilder<T>> inner)
    {
        WhereQueryBuilder<T>.Make(Source, inner);
        return this;
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Delete()
    {
        return Delete();
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Where(Action<IWhereQueryBuilder<T>> where)
    {
        return Where(where);
    }
}
