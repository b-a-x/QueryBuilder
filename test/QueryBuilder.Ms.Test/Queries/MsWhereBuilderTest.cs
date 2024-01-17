using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queris;

public class MsWhereBuilderTest
{
    [Theory]
    [InlineData("\r\nwhere tc.Name = @0 and ")]
    public void Where_BuildSql(string expected)
    {
        var source = new QueryBuilderContext();
        MsWhereBuilder<TestClass>.MakeWhere(source, x => x.EqualTo(x => x.Name, "test").And());
        Assert.Equal(expected, source.Query.ToString());
    }
}
