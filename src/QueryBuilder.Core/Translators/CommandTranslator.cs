using QueryBuilder.Core.Queris;

namespace QueryBuilder.Core.Translators;

public abstract class CommandTranslator : Translator
{
    protected readonly string command;
    internal CommandTranslator(string command)
    {
        this.command = command;
    }

    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append("\r\n").Append(command).Append(" ");
    }
}
