using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;
using QueryBuilder.Test;

namespace QueryBuilder.Ms.Test.Queris;

public class MsDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete dbo.TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source).Delete();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass\r\ndelete dbo.TestClass")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source).Delete().Delete();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass\r\nwhere Id = @0 and Name = @1 and Age = @2 and Timespan = @3")]
    public void DeleteWhere_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsDeleteQueryBuilder<TestClass>(source)
           .Delete()
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, source.Query.ToString());
    }
}
