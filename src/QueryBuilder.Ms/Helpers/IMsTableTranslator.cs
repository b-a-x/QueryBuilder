namespace QueryBuilder.Ms.Helpers;

public interface IMsTableTranslator
{
    static virtual string Schema { get => "dbo"; }
    static virtual string Table { get; }
    static virtual string Alias { get; }
}

