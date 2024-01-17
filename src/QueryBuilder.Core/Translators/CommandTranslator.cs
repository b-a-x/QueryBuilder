using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct CommandTranslator
{
    private readonly string _command;
    public CommandTranslator(string command) { _command = command; }

    public void Run(QueryBuilderContext source)
    {
        source.Query.Append("\r\n").Append(_command).Append(" ");
    }
}