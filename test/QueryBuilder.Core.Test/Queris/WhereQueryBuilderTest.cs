using QueryBuilder.Core.Queris;
using QueryBuilder.Test;

namespace QueryBuilder.Core.Test.Queris;

public class WhereQueryBuilderTest
{
    [Theory]
    [InlineData("\r\nwhere Name = @0 and ")]
    public void Where_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new WhereQueryBuilder<TestClass>(source).Where().EqualTo(x => x.Name, "test").And();
        Assert.Equal(expected, source.Query.ToString());
    }
}
