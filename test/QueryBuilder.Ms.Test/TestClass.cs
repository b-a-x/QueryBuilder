using QueryBuilder.Core.Helpers;

namespace QueryBuilder.Ms.Test;

public class TestClass : IHasTable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime Timespan { get; set; }

    public static Table GetTable() => new Table("dbo", "TestClass", "tc");
}

public class MoreTestClass : TestClass, IHasTable
{
    public new static Table GetTable() => new Table("dbo", "MoreTestClass", "mtc");
}
