namespace QueryBuilder.Core.Translators;

public class SmartTableTranslator<T> : TableTranslator<T>
{
    public SmartTableTranslator(string command, string schema = "") : base(command, schema)
    {
    }

    public static SmartTableTranslator<T> Make(string command)
    {
        return new SmartTableTranslator<T>(command);
    }
}
