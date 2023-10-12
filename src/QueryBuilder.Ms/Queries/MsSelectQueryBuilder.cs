using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsSelectQueryBuilder<T>
{
    IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner);
}

public class MsSelectQueryBuilder<T> : QueryBuilderCore, IMsSelectQueryBuilder<T>
{
    public MsSelectQueryBuilder(QueryBuilderSource source) : base(source) {}

    public MsSelectQueryBuilder<T> Select()
    {
        CommandTranslator.Make("select").Run(Source);
        return this;
    }

    public MsSelectQueryBuilder<T> From()
    {
        CommandTranslator.Make("delete").Run(Source);
        return this;
    }

    public static MsSelectQueryBuilder<T> Make(QueryBuilderSource source, Action<MsSelectQueryBuilder<T>> inner)
    {
        var obj = new MsSelectQueryBuilder<T>(source);
        obj.Select();
        inner?.Invoke(obj);
        return obj;
    }

    public IMsSelectQueryBuilder<T> Select(Action<IMsSelectBuilder<T>> inner)
    {
        throw new NotImplementedException();
    }
}