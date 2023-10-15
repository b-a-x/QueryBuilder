using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Ms.Queries;

public interface IMsJoinBuilder<TLeft, TRigth>
{
    //IMsJoinBuilder<TLeft, TRigth> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    //IMsJoinBuilder<TLeft, TRigth> And();
}

public class MsJoinBuilder<TLeft, TRigth> : QueryBuilderCore, IMsJoinBuilder<TLeft, TRigth>
{
    protected MsJoinBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsJoinBuilder<TLeft, TRigth> Join()
    {
        CommandTranslator.Make("join").Run(Source);
        return this;
    }

    public static MsJoinBuilder<TLeft, TRigth> Make(QueryBuilderSource source, Action<MsJoinBuilder<TLeft, TRigth>> inner)
    {
        var obj = new MsJoinBuilder<TLeft, TRigth>(source).Join();
        inner?.Invoke(obj);
        return obj;
    }
}