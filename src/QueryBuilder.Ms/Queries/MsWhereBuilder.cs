using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Queries;

public interface IMsWhereBuilder<T> : IWhereBuilder<T>
    where T : ITableBuilder
{
    IMsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value);
    IMsWhereBuilder<T> And();
}

public class MsWhereBuilder : QueryBuilderCore
{
    protected MsWhereBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public void Where() 
        => CommandTranslator.Make("where").Run(Source);
}

public class MsWhereBuilder<T> : MsWhereBuilder, IMsWhereBuilder<T>
    where T : ITableBuilder
{
    public MsWhereBuilder(QueryBuilderSource source) : base(source) { }

    public MsWhereBuilder<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, [NotNull] TField value)
    {
        EqualToTranslator<T>.Make(CommonExpression.GetColumnName(column), value).Run(Source);
        return this;
    }

    public MsWhereBuilder<T> And()
    {
        AndTranslator.Make().Run(Source);
        return this;
    }

    public static MsWhereBuilder<T> Make(QueryBuilderSource source, Action<MsWhereBuilder<T>> inner)
    {
        var obj = new MsWhereBuilder<T>(source);
        inner?.Invoke(obj);
        return obj;
    }

    public static MsWhereBuilder<T> MakeWhere(QueryBuilderSource source, Action<MsWhereBuilder<T>> inner)
    {
        var obj = new MsWhereBuilder<T>(source);
        obj.Where();
        inner?.Invoke(obj);
        return obj;
    }

    IMsWhereBuilder<T> IMsWhereBuilder<T>.EqualTo<TField>(Expression<Func<T, TField>> column, TField value) =>
       EqualTo(column, value);

    IMsWhereBuilder<T> IMsWhereBuilder<T>.And() =>
       And();
}

public interface IMsWhereBuilder<TLeft, TRigth>
    where TLeft : ITableBuilder
    where TRigth : ITableBuilder
{
    IMsWhereBuilder<T> Bind<T>() where T : ITableBuilder;
}

public class MsWhereBuilder<TLeft, TRigth> : MsWhereBuilder, IMsWhereBuilder<TLeft, TRigth>
    where TLeft : ITableBuilder
    where TRigth : ITableBuilder
{
    public MsWhereBuilder(QueryBuilderSource source) : base(source)
    {
    }

    public MsWhereBuilder<T> Bind<T>() 
        where T : ITableBuilder
    {
        return MsWhereBuilder<T>.Make(Source, null);
    }

    IMsWhereBuilder<T> IMsWhereBuilder<TLeft, TRigth>.Bind<T>() 
        => Bind<T>();

    public static MsWhereBuilder<TLeft, TRigth> Make(QueryBuilderSource source, Action<MsWhereBuilder<TLeft, TRigth>> inner)
    {
        var obj = new MsWhereBuilder<TLeft, TRigth>(source);
        obj.Where();
        inner?.Invoke(obj);
        return obj;
    }
}