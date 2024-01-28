using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Translators;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface ISelect
{
    IQBSelect<T> Select<T>(Action<ISelectBuilder<T>> inner) where T : IHasTable;
}

public interface IQBSelect<TDto> : ISelect
    where TDto : IHasTable
{
    IQBSelect<TDto> Select(Action<ISelectBuilder<TDto>> inner);
    IQBSelect<TDto> From();
    IQBSelect<TDto> From(Action<IQBSelect<TDto>> inner);
    IQBSelect<TDto> Join<TRigth>(Action<IJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable;
    IQBSelect<TDto> Join<TLeft, TRigth>(Action<IJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable;
    IQBSelect<TDto> LeftJoin<TRigth>(Action<IJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable;
    IQBSelect<TDto> LeftJoin<TLeft, TRigth>(Action<IJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable;
    IQBSelect<TDto> Where(Action<IWhereBuilder<TDto>> inner);
}

public class QBSelect<TDto> : QBCore, IQBSelect<TDto>
    where TDto : IHasTable
{
    public IQBSelect<TDto> Select(Action<ISelectBuilder<TDto>> inner)
    {
        new CommandTranslator("select").Run(context);
        QBCore.Make<SelectBuilder<TDto>>(context, inner);
        return this;
    }

    public IQBSelect<T> Select<T>(Action<ISelectBuilder<T>> inner)
        where T : IHasTable
    {
        return QBCore.Make<QBSelect<T>>(context).Select(inner);
    }

    public IQBSelect<TDto> From()
    {
        new TableTranslator("from", TDto.GetTable()).Run(context);
        return this;
    }

    public IQBSelect<TDto> From(Action<IQBSelect<TDto>> inner)
    {
        var translator = new FromTranslator(TDto.GetTable());
        translator.BeginRun(context);
        QBCore.Make<QBSelect<TDto>>(context, inner);
        translator.EndRun(context);
        return this;
    }

    public IQBSelect<TDto> Where(Action<IWhereBuilder<TDto>> inner)
    {
        new CommandTranslator("where").Run(context);
        QBCore.Make<WhereBuilder<TDto>>(context, inner);
        return this;
    }

    public IQBSelect<TDto> Join<TRigth>(Action<IJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        new JoinTranslator("join", TRigth.GetTable()).Run(context);
        QBCore.Make<JoinBuilder<TDto, TRigth>>(context, inner);
        return this;
    }

    public IQBSelect<TDto> Join<TLeft, TRigth>(Action<IJoinBuilder<TLeft, TRigth>> inner)
        where TLeft : IHasTable
        where TRigth : IHasTable
    {
        new JoinTranslator("join", TRigth.GetTable()).Run(context);
        QBCore.Make<JoinBuilder<TLeft, TRigth>>(context, inner);
        return this;
    }

    public IQBSelect<TDto> LeftJoin<TRigth>(Action<IJoinBuilder<TDto, TRigth>> inner)
        where TRigth : IHasTable
    {
        new JoinTranslator("left join", TRigth.GetTable()).Run(context);
        QBCore.Make<JoinBuilder<TDto, TRigth>>(context, inner);
        return this;
    }

    public IQBSelect<TDto> LeftJoin<TLeft, TRigth>(Action<IJoinBuilder<TLeft, TRigth>> inner)
        where TRigth : IHasTable
        where TLeft : IHasTable
    {
        new JoinTranslator("left join", TRigth.GetTable()).Run(context);
        QBCore.Make<JoinBuilder<TLeft, TRigth>>(context, inner);
        return this;
    }
}