using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Ms.Translators;

namespace QueryBuilder.Ms.Queries;

public interface ICaseBuilder<TDto>
    where TDto : IHasTable
{
    ICaseBuilder<TDto> When(Action action);
    ICaseBuilder<TDto> Then(Action action);
    ICaseBuilder<TDto> Then<T>(T value);
    ICaseBuilder<TDto> Else(Action action);
    ICaseBuilder<TDto> Else<T>(T value);
}

public class CaseBuilder<TDto> : QBCore, ICaseBuilder<TDto>
    where TDto : IHasTable
{
    public void Case(Action<ICaseBuilder<TDto>> inner)
    {
        var translator = new CaseTranslator();
        translator.BeforeRun(context);
        inner?.Invoke(this);
        translator.AfterRun(context);
    }

    public ICaseBuilder<TDto> When(Action action)
    {
        new WhenTranslator().Run(context);
        action?.Invoke();
        return this;
    }

    public ICaseBuilder<TDto> Then(Action action)
    {
        new ThenTranslator().Run(context);
        action?.Invoke();
        return this;
    }

    public ICaseBuilder<TDto> Else(Action action)
    {
        new ElseTranslator().Run(context);
        action?.Invoke();
        return this;
    }

    public ICaseBuilder<TDto> Then<T>(T value)
    {
        new ThenTranslator().Run(context);
        context.Query.Append(value.ToString());
        return this;
    }

    public ICaseBuilder<TDto> Else<T>(T value)
    {
        new ElseTranslator().Run(context);
        context.Query.Append(value.ToString());
        return this;
    }
}
