using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queris;

public class MsWhereBuilderTest
{
    [Theory]
    [InlineData("\r\nwhere tc.Name = @0 and ")]
    public void Where_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsWhereBuilder<TestClass>(source).Where().EqualTo(x => x.Name, "test").And();
        Assert.Equal(expected, source.Query.ToString());
    }
}
