using QueryBuilder.Ms.Helpers;

namespace QueryBuilder.Ms.Test;

public class TestClass : IMsTableTranslator
{
    public static string Table => "TestClass";
    public static string Alias => "tc";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime Timespan { get; set; }
}

public class MoreTestClass : TestClass , IMsTableTranslator
{
    public new static string Table => "MoreTestClass";
    public new static string Alias => "mtc";
}
