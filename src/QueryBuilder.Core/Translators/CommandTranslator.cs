using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public class CommandTranslator : Translator
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

    public static CommandTranslator Make(string command)
    {
        return new CommandTranslator(command);
    }
}
