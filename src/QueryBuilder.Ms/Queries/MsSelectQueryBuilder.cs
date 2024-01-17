using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface ISelect
{
    IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner) where T : IHasTable;
}

public interface IMsSelectQueryBuilder<TDto> : ISelect
    where TDto : IHasTable
{
    IMsSelectQueryBuilder<TDto> Select(Action<IMsSelectBuilder<TDto>> inner);
    IMsSelectQueryBuilder<TDto> From();
    IMsSelectQueryBuilder<TDto> From(Action<IMsSelectQueryBuilder<TDto>> inner);
    IMsSelectQueryBuilder<TDto> Join<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable;
    IMsSelectQueryBuilder<TDto> Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable;
    IMsSelectQueryBuilder<TDto> LeftJoin<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable;
    IMsSelectQueryBuilder<TDto> LeftJoin<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable;
    IMsSelectQueryBuilder<TDto> Where(Action<IMsWhereBuilder<TDto>> inner);
}

public class MsSelectQueryBuilder<TDto> : QueryBuilderCore, IMsSelectQueryBuilder<TDto>
    where TDto : IHasTable
{
    public MsSelectQueryBuilder(QueryBuilderContext source) : base(source) {}

    public MsSelectQueryBuilder<TDto> Select()
    {
        new CommandTranslator("select").Run(Context);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Select(Action<MsSelectBuilder<TDto>> inner)
    {
        Select();
        MsSelectBuilder<TDto>.Make(Context, inner);
        return this;
    }

    public MsSelectQueryBuilder<T> Select<T>(Action<MsSelectBuilder<T>> inner)
        where T : IHasTable
    {
        return MsSelectQueryBuilder<T>.Make(Context, null).Select(inner);
    }

    public MsSelectQueryBuilder<TDto> From()
    {
        new TableTranslator("from", TDto.GetTable()).Run(Context);
        return this;
    }

    public MsSelectQueryBuilder<TDto> From(Action<MsSelectQueryBuilder<TDto>> inner)
    {
        var translator = new FromTranslator(TDto.GetTable());
        translator.BeginRun(Context);
        MsSelectQueryBuilder<TDto>.Make(Context, inner);
        translator.EndRun(Context);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Where(Action<MsWhereBuilder<TDto>> inner)
    {
        MsWhereBuilder<TDto>.MakeWhere(Context, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Join<TRigth>(Action<MsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        MsJoinBuilder<TDto, TRigth>.JoinMake(Context, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Join<TLeft, TRigth>(Action<MsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable
    {
        MsJoinBuilder<TLeft, TRigth>.JoinMake(Context, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> LeftJoin<TRigth>(Action<MsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        MsJoinBuilder<TDto, TRigth>.LeftJoinMake(Context, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> LeftJoin<TLeft, TRigth>(Action<MsJoinBuilder<TLeft, TRigth>> inner)
        where TRigth : IHasTable
        where TLeft : IHasTable
    {
        MsJoinBuilder<TLeft, TRigth>.LeftJoinMake(Context, inner);
        return this;
    }

    public static MsSelectQueryBuilder<TDto> Make(QueryBuilderContext source, Action<MsSelectQueryBuilder<TDto>> inner)
    {
        var obj = new MsSelectQueryBuilder<TDto>(source);
        inner?.Invoke(obj);
        return obj;
    }

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.Where(Action<IMsWhereBuilder<TDto>> inner)
        => Where(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner) 
        => Join(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.Join<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner) 
        => Join<TRigth>(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.LeftJoin<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner) 
        => LeftJoin<TRigth>(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.LeftJoin<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner) 
        => LeftJoin(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.From(Action<IMsSelectQueryBuilder<TDto>> inner) 
        => From(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.From() 
        => From();

    IMsSelectQueryBuilder<T> ISelect.Select<T>(Action<IMsSelectBuilder<T>> inner) 
        => Select(inner);

    IMsSelectQueryBuilder<TDto> IMsSelectQueryBuilder<TDto>.Select(Action<IMsSelectBuilder<TDto>> inner) 
        => Select(inner);
}