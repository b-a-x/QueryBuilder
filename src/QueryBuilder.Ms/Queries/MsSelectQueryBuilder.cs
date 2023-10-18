using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface ISelect
{
    IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner) where T : ITableBuilder;
}

public interface IMsSelectQueryBuilder<TDto> : ISelect
    where TDto : ITableBuilder
{
    IMsSelectQueryBuilder<TDto> Select(Action<IMsSelectBuilder<TDto>> inner);
    IMsSelectQueryBuilder<TDto> From();
    IMsSelectQueryBuilder<TDto> From(Action<IMsSelectQueryBuilder<TDto>> inner);
    IMsSelectQueryBuilder<TDto> Join<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<TDto> Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : ITableBuilder
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<TDto> LeftJoin<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<TDto> LeftJoin<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : ITableBuilder
        where TRigth : ITableBuilder;
    IMsSelectQueryBuilder<TDto> Where(Action<IMsWhereBuilder<TDto>> inner);
}

public class MsSelectQueryBuilder<TDto> : QueryBuilderCore, IMsSelectQueryBuilder<TDto>
    where TDto : ITableBuilder
{
    public MsSelectQueryBuilder(QueryBuilderSource source) : base(source) {}

    public MsSelectQueryBuilder<TDto> Select()
    {
        new CommandTranslator("select").Run(Source);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Select(Action<MsSelectBuilder<TDto>> inner)
    {
        Select();
        MsSelectBuilder<TDto>.Make(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<T> Select<T>(Action<MsSelectBuilder<T>> inner)
        where T : ITableBuilder
    {
        return MsSelectQueryBuilder<T>.Make(Source, null).Select(inner);
    }

    public MsSelectQueryBuilder<TDto> From()
    {
        new TableTranslator("from", TDto.GetTable()).Run(Source);
        return this;
    }

    public MsSelectQueryBuilder<TDto> From(Action<MsSelectQueryBuilder<TDto>> inner)
    {
        var translator = new FromTranslator(TDto.GetTable());
        translator.BeginRun(Source);
        MsSelectQueryBuilder<TDto>.Make(Source, inner);
        translator.EndRun(Source);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Where(Action<MsWhereBuilder<TDto>> inner)
    {
        MsWhereBuilder<TDto>.MakeWhere(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Join<TRigth>(Action<MsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : ITableBuilder
    {
        MsJoinBuilder<TDto, TRigth>.JoinMake(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> Join<TLeft, TRigth>(Action<MsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : ITableBuilder
        where TRigth : ITableBuilder
    {
        MsJoinBuilder<TLeft, TRigth>.JoinMake(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> LeftJoin<TRigth>(Action<MsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : ITableBuilder
    {
        MsJoinBuilder<TDto, TRigth>.LeftJoinMake(Source, inner);
        return this;
    }

    public MsSelectQueryBuilder<TDto> LeftJoin<TLeft, TRigth>(Action<MsJoinBuilder<TLeft, TRigth>> inner)
        where TRigth : ITableBuilder
        where TLeft : ITableBuilder
    {
        MsJoinBuilder<TLeft, TRigth>.LeftJoinMake(Source, inner);
        return this;
    }

    public static MsSelectQueryBuilder<TDto> Make(QueryBuilderSource source, Action<MsSelectQueryBuilder<TDto>> inner)
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