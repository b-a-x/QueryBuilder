using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Test;

public class TestClass : ITableBuilder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime Timespan { get; set; }

    public static TableBuilder GetTable() => new TableBuilder("dbo", "TestClass", "tc");
}

public class MoreTestClass : TestClass, ITableBuilder
{
    public new static TableBuilder GetTable() => new TableBuilder("dbo", "MoreTestClass", "mtc");
}
