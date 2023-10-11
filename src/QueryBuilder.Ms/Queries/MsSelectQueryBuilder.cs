namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
}

public class MsSelectQueryBuilder<T> : IMsSelectQueryBuilder<T>
{
    IMsSelectQueryBuilder<T> IMsSelectQueryBuilder<T>.Select(Action<IMsSelectBuilder<T>> inner)
    {
        throw new NotImplementedException();
    }
}
