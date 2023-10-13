using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queries;

public class MsSelectQueryBuilderTest
{
    [Theory]
    [InlineData("\r\nselect * \r\nfrom dbo.TestClass as tc")]
    public void Select_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All());
        Assert.Equal(expected, source.Query.ToString());
    }
}
