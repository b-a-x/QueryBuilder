using QueryBuilder.Core.Queris;
using QueryBuilder.Test;

namespace QueryBuilder.Core.Test.Queris;

public class DeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new DeleteQueryBuilder<TestClass>(source).Delete();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete TestClass\r\ndelete TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new DeleteQueryBuilder<TestClass>(source).Delete().Delete();
        Assert.Equal(expected, source.Query.ToString());
    }
}
