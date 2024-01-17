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

public class MsSelectQueryBuilder<TDto> : QBCore, IMsSelectQueryBuilder<TDto>
    where TDto : IHasTable
{
    public IMsSelectQueryBuilder<TDto> Select(Action<IMsSelectBuilder<TDto>> inner)
    {
        new CommandTranslator("select").Run(Context);
        QBCore.Make<MsSelectBuilder<TDto>>(Context, inner);
        return this;
    }

    public IMsSelectQueryBuilder<T> Select<T>(Action<IMsSelectBuilder<T>> inner)
        where T : IHasTable
    {
        return QBCore.Make<MsSelectQueryBuilder<T>>(Context).Select(inner);
    }

    public IMsSelectQueryBuilder<TDto> From()
    {
        new TableTranslator("from", TDto.GetTable()).Run(Context);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> From(Action<IMsSelectQueryBuilder<TDto>> inner)
    {
        var translator = new FromTranslator(TDto.GetTable());
        translator.BeginRun(Context);
        QBCore.Make<MsSelectQueryBuilder<TDto>>(Context, inner);
        translator.EndRun(Context);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> Where(Action<IMsWhereBuilder<TDto>> inner)
    {
        new CommandTranslator("where").Run(Context);
        QBCore.Make<MsWhereBuilder<TDto>>(Context, inner);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> Join<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        new JoinTranslator("join", TRigth.GetTable()).Run(Context);
        QBCore.Make<MsJoinBuilder<TDto, TRigth>>(Context, inner);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> Join<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable
    {
        new JoinTranslator("join", TRigth.GetTable()).Run(Context);
        QBCore.Make<MsJoinBuilder<TLeft, TRigth>>(Context, inner);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> LeftJoin<TRigth>(Action<IMsJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        new JoinTranslator("left join", TRigth.GetTable()).Run(Context);
        QBCore.Make<MsJoinBuilder<TDto, TRigth>>(Context, inner);
        return this;
    }

    public IMsSelectQueryBuilder<TDto> LeftJoin<TLeft, TRigth>(Action<IMsJoinBuilder<TLeft, TRigth>> inner)
        where TRigth : IHasTable
        where TLeft : IHasTable
    {
        new JoinTranslator("left join", TRigth.GetTable()).Run(Context);
        QBCore.Make<MsJoinBuilder<TLeft, TRigth>>(Context, inner);
        return this;
    }
}